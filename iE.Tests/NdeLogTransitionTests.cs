using iE.Api.Controllers;
using iE.Api.Workflow;
using Microsoft.AspNetCore.Mvc;

public class NdeLogTransitionTests
{
    [Fact]
    public void Valid_Transition_Succeeds_And_Creates_Event()
    {
        var service = new DemoNdeLogReadModelService();
        var controller = new NdeLogController(service);

        var result = controller.Transition("nde-001", new NdeLogTransitionCommand(NdeLogStatuses.Requested, "ready", "qa.user"));
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var row = Assert.IsType<NdeLogItemReadModel>(ok.Value);

        Assert.Equal(NdeLogStatuses.Requested, row.Status);
        var events = service.GetEvents("nde-001");
        var evt = Assert.Single(events);
        Assert.Equal(NdeLogStatuses.Draft, evt.FromStatus);
        Assert.Equal(NdeLogStatuses.Requested, evt.ToStatus);
        Assert.Equal("qa.user", evt.Actor);
        Assert.Equal("ready", evt.Comment);
        Assert.True(evt.TimestampUtc <= DateTime.UtcNow);
    }

    [Fact]
    public void Get_Events_Returns_Created_Event()
    {
        var service = new DemoNdeLogReadModelService();
        var controller = new NdeLogController(service);

        var transition = controller.Transition("nde-001", new NdeLogTransitionCommand(NdeLogStatuses.Requested, "approved", "nde.coordinator"));
        Assert.IsType<OkObjectResult>(transition.Result);

        var response = controller.GetEvents("nde-001");
        var ok = Assert.IsType<OkObjectResult>(response.Result);
        var events = Assert.IsAssignableFrom<IReadOnlyList<NdeLogTransitionEventReadModel>>(ok.Value);
        var evt = Assert.Single(events);

        Assert.Equal("nde-001", evt.NdeRequestId);
        Assert.Equal(NdeLogStatuses.Draft, evt.FromStatus);
        Assert.Equal(NdeLogStatuses.Requested, evt.ToStatus);
        Assert.Equal("nde.coordinator", evt.Actor);
        Assert.Equal("approved", evt.Comment);
        Assert.True(evt.TimestampUtc <= DateTime.UtcNow);
    }

    [Fact]
    public void Invalid_Transition_Fails()
    {
        var controller = new NdeLogController(new DemoNdeLogReadModelService());
        var result = controller.Transition("nde-001", new NdeLogTransitionCommand(NdeLogStatuses.Closed, null, null));
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public void Transition_For_Unknown_Id_Returns_NotFound()
    {
        var controller = new NdeLogController(new DemoNdeLogReadModelService());
        var result = controller.Transition("nde-999", new NdeLogTransitionCommand(NdeLogStatuses.Requested, null, "demo.user"));
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public void Get_Events_For_Unknown_Id_Returns_NotFound()
    {
        var controller = new NdeLogController(new DemoNdeLogReadModelService());
        var result = controller.GetEvents("nde-999");
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void Get_Events_For_Known_Id_Without_Events_Returns_Empty_List()
    {
        var controller = new NdeLogController(new DemoNdeLogReadModelService());
        var result = controller.GetEvents("nde-002");
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var events = Assert.IsAssignableFrom<IReadOnlyList<NdeLogTransitionEventReadModel>>(ok.Value);
        Assert.Empty(events);
    }

        [Fact]
    public void Closed_Or_Cancelled_Cannot_Transition()
    {
        var service = new DemoNdeLogReadModelService();
        var controller = new NdeLogController(service);

        var closedResult = controller.Transition("nde-007", new NdeLogTransitionCommand(NdeLogStatuses.Requested, null, "demo.user"));
        Assert.IsType<BadRequestObjectResult>(closedResult.Result);

        var cancelOk = controller.Transition("nde-002", new NdeLogTransitionCommand(NdeLogStatuses.Cancelled, null, "demo.user"));
        Assert.IsType<OkObjectResult>(cancelOk.Result);
        var cancelledResult = controller.Transition("nde-002", new NdeLogTransitionCommand(NdeLogStatuses.Scheduled, null, "demo.user"));
        Assert.IsType<BadRequestObjectResult>(cancelledResult.Result);
    }

    [Fact]
    public void Nde_Routes_Are_Separated_From_Api_Inspection_Reports()
    {
        var controllerType = typeof(NdeLogController);
        var route = Assert.Single(controllerType.GetCustomAttributes(typeof(RouteAttribute), false).Cast<RouteAttribute>());
        Assert.Equal("api/nde/log", route.Template);
    }
}
