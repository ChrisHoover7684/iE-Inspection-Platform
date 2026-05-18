import { fireEvent, render, screen } from "@testing-library/react";
import { afterEach, describe, expect, it, vi } from "vitest";
import { Api510ShellTubeCalculationWorkspace } from "../reporting/Api510ShellTubeCalculationWorkspace";
import { executeApi510ComponentCalculation } from "../reporting/componentCalculationExecution";
import { pressureVesselApi } from "../api";
import { API_INSPECTION_DRAFT_SETUP_STORAGE_KEY } from "../reporting/componentCalculationPrefill";

vi.mock("../reporting/componentCalculationExecution", async () => {
  const actual = await vi.importActual<
    typeof import("../reporting/componentCalculationExecution")
  >("../reporting/componentCalculationExecution");
  return { ...actual, executeApi510ComponentCalculation: vi.fn() };
});

const mockResolveAllowableStress = async (stress = 17500) => {
  vi.spyOn(pressureVesselApi, "resolveMaterialStress").mockResolvedValueOnce({
    isValid: true,
    allowableStressPsi: stress,
    materialMatched: "SA-516 70 Plate",
    temperatureUsed: 450,
    wasInterpolated: false,
    wasExtrapolated: false,
    message: "ok",
    warnings: [],
  });
  fireEvent.click(
    screen.getByRole("button", { name: "Resolve Allowable Stress" }),
  );
  await screen.findByText(`Resolved Allowable Stress (psi): ${stress}`);
};

afterEach(() => {
  window.localStorage.removeItem(API_INSPECTION_DRAFT_SETUP_STORAGE_KEY);
  vi.restoreAllMocks();
});

describe("Api510ShellTubeCalculationWorkspace", () => {
  it("shows lookup controls and keeps resolved stress read-only", () => {
    render(<Api510ShellTubeCalculationWorkspace />);
    expect(
      screen.getByRole("button", { name: "Resolve Allowable Stress" }),
    ).toBeInTheDocument();
    expect(
      screen.queryByRole("textbox", {
        name: "Resolved Allowable Stress (psi)",
      }),
    ).not.toBeInTheDocument();
    expect(
      screen.getByLabelText("Resolved Allowable Stress (psi)"),
    ).toHaveTextContent("Unresolved");
  });

  it("blocks calculation while stress is unresolved and allows running after lookup", async () => {
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({
      success: true,
      calculationType: "ug-27-shell-tmin",
      componentKey: "shell",
      componentLabel: "Shell",
      pressureSide: "shell-side",
      inputsUsed: {},
      resultSummary: "ok",
      warnings: [],
    } as any);
    render(
      <Api510ShellTubeCalculationWorkspace
        fieldValues={{
          "api510.external.exchanger.shell-tube.shell-side.design-pressure": 100,
          "api510.external.exchanger.shell-tube.shell-side.design-temperature": 450,
        }}
      />,
    );
    expect(
      screen.getByRole("button", { name: "Run Calculation" }),
    ).toBeDisabled();
    await mockResolveAllowableStress();
    expect(
      screen.getByRole("button", { name: "Run Calculation" }),
    ).toBeEnabled();
    fireEvent.click(screen.getByRole("button", { name: "Run Calculation" }));
    expect(executeApi510ComponentCalculation).toHaveBeenCalled();
  });

  it("shell-side lookup uses shell material and shell-side design temperature", async () => {
    const spy = vi
      .spyOn(pressureVesselApi, "resolveMaterialStress")
      .mockResolvedValueOnce({
        isValid: true,
        allowableStressPsi: 17500,
        materialMatched: "shell",
        temperatureUsed: 450,
        wasInterpolated: false,
        wasExtrapolated: false,
        message: "ok",
        warnings: [],
      });
    render(
      <Api510ShellTubeCalculationWorkspace
        fieldValues={{
          "api510.external.exchanger.shell-tube.shell-side.design-pressure": 100,
          "api510.external.exchanger.shell-tube.shell-side.design-temperature": 450,
        }}
      />,
    );
    fireEvent.change(screen.getByLabelText("Shell Material Specification"), {
      target: { value: "SA-516" },
    });
    fireEvent.change(screen.getByLabelText("Shell Grade"), {
      target: { value: "70" },
    });
    fireEvent.change(screen.getByLabelText("Shell Product Form"), {
      target: { value: "Plate" },
    });
    fireEvent.click(
      screen.getByRole("button", { name: "Resolve Allowable Stress" }),
    );
    await screen.findByText("Resolved Allowable Stress (psi): 17500");
    expect(spy).toHaveBeenCalledWith(
      expect.objectContaining({
        materialSpec: "SA-516",
        materialGrade: "70",
        productForm: "Plate",
        designTemperatureF: 450,
      }),
    );
  });

  it("tube-side lookup uses tube/channel material and tube-side design temperature", async () => {
    const spy = vi
      .spyOn(pressureVesselApi, "resolveMaterialStress")
      .mockResolvedValueOnce({
        isValid: true,
        allowableStressPsi: 18000,
        materialMatched: "tube",
        temperatureUsed: 625,
        wasInterpolated: true,
        wasExtrapolated: false,
        message: "ok",
        warnings: [],
      });
    render(
      <Api510ShellTubeCalculationWorkspace
        fieldValues={{
          "api510.external.exchanger.shell-tube.tube-side.design-pressure": 100,
          "api510.external.exchanger.shell-tube.tube-side.design-temperature": 625,
        }}
      />,
    );
    fireEvent.change(screen.getByLabelText("Component"), { target: { value: "nozzles" } });
    fireEvent.change(screen.getByLabelText("Parent Component"), { target: { value: "channel-channel-head" } });
    fireEvent.change(screen.getByLabelText("Pressure Side"), {
      target: { value: "tube-side" },
    });
    fireEvent.change(
      screen.getByLabelText("Tube/Channel Material Specification"),
      { target: { value: "SA-213" } },
    );
    fireEvent.change(screen.getByLabelText("Tube/Channel Grade"), {
      target: { value: "TP304" },
    });
    fireEvent.change(screen.getByLabelText("Tube/Channel Product Form"), {
      target: { value: "Tube" },
    });
    fireEvent.click(
      screen.getByRole("button", { name: "Resolve Allowable Stress" }),
    );
    await screen.findByText("Resolved Allowable Stress (psi): 18000");
    expect(spy).toHaveBeenCalledWith(
      expect.objectContaining({
        materialSpec: "SA-213",
        materialGrade: "TP304",
        productForm: "Tube",
        designTemperatureF: 625,
      }),
    );
  });

  it("manual override requires a reason before calculation can run", () => {
    render(
      <Api510ShellTubeCalculationWorkspace
        fieldValues={{
          "api510.external.exchanger.shell-tube.shell-side.design-pressure": 100,
          "api510.external.exchanger.shell-tube.shell-side.design-temperature": 450,
        }}
      />,
    );
    fireEvent.click(
      screen.getByLabelText("Use manual allowable stress override"),
    );
    fireEvent.change(screen.getByLabelText("Manual Allowable Stress (psi)"), {
      target: { value: "17500" },
    });
    expect(
      screen.getByRole("button", { name: "Run Calculation" }),
    ).toBeDisabled();
    fireEvent.change(screen.getByLabelText("Manual Override Reason"), {
      target: { value: "verified table lookup unavailable" },
    });
    expect(
      screen.getByRole("button", { name: "Run Calculation" }),
    ).toBeEnabled();
  });

  it("workspace renders component selector", () => {
    render(<Api510ShellTubeCalculationWorkspace />);
    expect(screen.getByLabelText("Component")).toBeInTheDocument();
  });

  it("reads draft shell and tube design conditions and selected components", () => {
    window.localStorage.setItem(
      API_INSPECTION_DRAFT_SETUP_STORAGE_KEY,
      JSON.stringify({
        reportTypeId: "api510.external.exchanger.shell-tube",
        reportTypeLabel: "Shell and Tube Exchanger External",
        header: {
          shellSideDesignPressure: "150",
          shellSideDesignTemperature: "450",
          tubeSideDesignPressure: "275",
          tubeSideDesignTemperature: "625",
        },
        components: [
          "Shell",
          "Channel / Channel Head",
          "Nozzles",
          "Shell Cover",
        ],
      }),
    );

    render(<Api510ShellTubeCalculationWorkspace />);

    expect(screen.getByRole("status")).toHaveTextContent(
      "Using draft setup from Start Wizard",
    );
    expect(
      screen.getByText(
        "Design Pressure: api510.external.exchanger.shell-tube.shell-side.design-pressure = 150",
      ),
    ).toBeInTheDocument();
    expect(
      screen.getByText(
        "Design Temperature: api510.external.exchanger.shell-tube.shell-side.design-temperature = 450",
      ),
    ).toBeInTheDocument();
    fireEvent.change(screen.getByLabelText("Component"), {
      target: { value: "nozzles" },
    });
    fireEvent.change(screen.getByLabelText("Parent Component"), {
      target: { value: "channel-channel-head" },
    });
    fireEvent.change(screen.getByLabelText("Pressure Side"), {
      target: { value: "tube-side" },
    });
    expect(
      screen.getByText(
        "Design Pressure: api510.external.exchanger.shell-tube.tube-side.design-pressure = 275",
      ),
    ).toBeInTheDocument();
    expect(
      screen.getByText(
        "Design Temperature: api510.external.exchanger.shell-tube.tube-side.design-temperature = 625",
      ),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("region", { name: "Draft Context" }),
    ).toHaveTextContent("Shell Cover");
  });

  it("shows user-facing API 510 shell-and-tube labels, cards, dropdowns, and helper text", () => {
    render(<Api510ShellTubeCalculationWorkspace />);

    expect(
      screen.getByRole("group", { name: "Component Selection" }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("region", { name: "Resolved Design Conditions" }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("group", { name: "Material / Allowable Stress" }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("group", { name: "Calculation Inputs" }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("region", { name: "Results / Snapshot" }),
    ).toBeInTheDocument();
    expect(
      screen.getByText(
        "Allowable stress should be resolved from material tables. Manual override requires a reason.",
      ),
    ).toBeInTheDocument();
    expect(
      screen.getByText(
        "Save Snapshot is enabled only when this workspace is opened from a report context.",
      ),
    ).toBeInTheDocument();
    expect(screen.getByLabelText("Joint Efficiency")).toBeInTheDocument();
    expect(screen.getByLabelText("Inside Diameter (in.)")).toBeInTheDocument();
    expect(screen.getByLabelText("Outside Diameter (in.)")).toBeInTheDocument();
    expect(
      screen.getByLabelText("Original/Nominal Thickness (in.)"),
    ).toBeInTheDocument();
    expect(
      screen.getByLabelText("Current / Provided Thickness (in.)"),
    ).toBeInTheDocument();
    expect(
      screen.getByLabelText("Corrosion Allowance (in.)"),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("combobox", { name: "Diameter Basis" }),
    ).toHaveTextContent("Both ID and OD known");
    expect(screen.queryByText("jointEfficiency")).not.toBeInTheDocument();
    expect(screen.queryByText("insideDiameterIn")).not.toBeInTheDocument();
    expect(screen.queryByText("outsideDiameterIn")).not.toBeInTheDocument();
  });

  it("Save Snapshot enabled only with report context and snapshot payload; callback receives snapshot", async () => {
    const onSave = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({
      success: true,
      calculationType: "ug-27-shell-tmin",
      componentKey: "shell",
      componentLabel: "Shell",
      pressureSide: "shell-side",
      inputsUsed: {},
      resultSummary: "ok",
      warnings: [],
      snapshotReadyPayload: { id: "snap-1" },
    } as any);
    render(
      <Api510ShellTubeCalculationWorkspace
        hasReportContext
        onSaveSnapshot={onSave}
        fieldValues={{
          "api510.external.exchanger.shell-tube.shell-side.design-pressure": 100,
          "api510.external.exchanger.shell-tube.shell-side.design-temperature": 100,
        }}
      />,
    );
    const save = screen.getByRole("button", {
      name: "Save Calculation Snapshot",
    });
    expect(save).toBeDisabled();
    await mockResolveAllowableStress();
    fireEvent.click(screen.getByRole("button", { name: "Run Calculation" }));
    expect(
      await screen.findByText(/Snapshot Preview: available/i),
    ).toBeInTheDocument();
    expect(save).toBeEnabled();
    fireEvent.click(save);
    expect(onSave).toHaveBeenCalledWith(
      expect.objectContaining({ id: "snap-1" }),
    );
  });

  it("duplicate snapshot save is prevented", async () => {
    const onSave = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({
      success: true,
      calculationType: "ug-27-shell-tmin",
      componentKey: "shell",
      componentLabel: "Shell",
      pressureSide: "shell-side",
      inputsUsed: {},
      resultSummary: "ok",
      warnings: [],
      snapshotReadyPayload: { id: "snap-dup" },
    } as any);
    render(
      <Api510ShellTubeCalculationWorkspace
        hasReportContext
        onSaveSnapshot={onSave}
        fieldValues={{
          "api510.external.exchanger.shell-tube.shell-side.design-pressure": 100,
          "api510.external.exchanger.shell-tube.shell-side.design-temperature": 100,
        }}
      />,
    );
    await mockResolveAllowableStress();
    fireEvent.click(screen.getByRole("button", { name: "Run Calculation" }));
    await screen.findByText(/Snapshot Preview: available/i);
    const save = screen.getByRole("button", {
      name: "Save Calculation Snapshot",
    });
    fireEvent.click(save);
    fireEvent.click(save);
    expect(onSave).toHaveBeenCalledTimes(1);
  });

  it("finding before saving marks linkedSnapshotPendingSave true", async () => {
    const onCreate = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({
      success: true,
      calculationType: "ug-27-shell-tmin",
      componentKey: "shell",
      componentLabel: "Shell",
      pressureSide: "shell-side",
      designPressureUsed: 150,
      designTemperatureUsed: 400,
      inputsUsed: {},
      resultSummary: "UG-27 calculation executed.",
      warnings: [],
      snapshotReadyPayload: {
        id: "s1",
        inputs: { prefill: { equipmentSubtype: "Shell and Tube Exchanger" } },
      },
    } as any);
    render(
      <Api510ShellTubeCalculationWorkspace
        hasReportContext
        onCreateFindingDraft={onCreate}
        fieldValues={{
          "api510.external.exchanger.shell-tube.shell-side.design-pressure": 100,
          "api510.external.exchanger.shell-tube.shell-side.design-temperature": 100,
        }}
      />,
    );
    await mockResolveAllowableStress();
    fireEvent.click(screen.getByRole("button", { name: "Run Calculation" }));
    await screen.findByText(/Execution: success/i);
    fireEvent.click(
      screen.getByRole("button", { name: "Create Finding from Calculation" }),
    );
    expect(onCreate).toHaveBeenCalledWith(
      expect.objectContaining({
        componentKey: "shell",
        linkedCalculationSnapshotId: undefined,
        linkedSnapshotPendingSave: true,
      }),
    );
  });

  it("finding after saving includes linkedCalculationSnapshotId", async () => {
    const onCreate = vi.fn();
    const onSave = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({
      success: true,
      calculationType: "ug-27-shell-tmin",
      componentKey: "shell",
      componentLabel: "Shell",
      pressureSide: "shell-side",
      designPressureUsed: 150,
      designTemperatureUsed: 400,
      inputsUsed: {},
      resultSummary: "UG-27 calculation executed.",
      warnings: [],
      snapshotReadyPayload: {
        id: "s-saved",
        inputs: { prefill: { equipmentSubtype: "Shell and Tube Exchanger" } },
      },
    } as any);
    render(
      <Api510ShellTubeCalculationWorkspace
        hasReportContext
        onSaveSnapshot={onSave}
        onCreateFindingDraft={onCreate}
        fieldValues={{
          "api510.external.exchanger.shell-tube.shell-side.design-pressure": 100,
          "api510.external.exchanger.shell-tube.shell-side.design-temperature": 100,
        }}
      />,
    );
    await mockResolveAllowableStress();
    fireEvent.click(screen.getByRole("button", { name: "Run Calculation" }));
    await screen.findByText(/Execution: success/i);
    fireEvent.click(
      screen.getByRole("button", { name: "Save Calculation Snapshot" }),
    );
    fireEvent.click(
      screen.getByRole("button", { name: "Create Finding from Calculation" }),
    );
    expect(onCreate).toHaveBeenCalledWith(
      expect.objectContaining({
        linkedCalculationSnapshotId: "s-saved",
        linkedSnapshotPendingSave: false,
      }),
    );
  });

  it("finding with existing reportCalculations snapshot includes linkedCalculationSnapshotId", async () => {
    const onCreate = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({
      success: true,
      calculationType: "ug-27-shell-tmin",
      componentKey: "shell",
      componentLabel: "Shell",
      pressureSide: "shell-side",
      designPressureUsed: 150,
      designTemperatureUsed: 400,
      inputsUsed: {},
      resultSummary: "UG-27 calculation executed.",
      warnings: [],
      snapshotReadyPayload: {
        id: "s-existing",
        inputs: { prefill: { equipmentSubtype: "Shell and Tube Exchanger" } },
      },
    } as any);
    render(
      <Api510ShellTubeCalculationWorkspace
        hasReportContext
        reportCalculations={[{ id: "s-existing" } as any]}
        onCreateFindingDraft={onCreate}
        fieldValues={{
          "api510.external.exchanger.shell-tube.shell-side.design-pressure": 100,
          "api510.external.exchanger.shell-tube.shell-side.design-temperature": 100,
        }}
      />,
    );
    await mockResolveAllowableStress();
    fireEvent.click(screen.getByRole("button", { name: "Run Calculation" }));
    await screen.findByText(/Execution: success/i);
    fireEvent.click(
      screen.getByRole("button", { name: "Create Finding from Calculation" }),
    );
    expect(onCreate).toHaveBeenCalledWith(
      expect.objectContaining({
        linkedCalculationSnapshotId: "s-existing",
        linkedSnapshotPendingSave: false,
      }),
    );
  });

  it("UG-45 finding includes parent component and nozzle location", async () => {
    const onCreate = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({
      success: true,
      calculationType: "ug-45-nozzle-neck-tmin",
      componentKey: "nozzles",
      componentLabel: "Nozzles",
      pressureSide: "shell-side",
      inputsUsed: {},
      resultSummary: "UG-45 calculation executed.",
      warnings: [],
      snapshotReadyPayload: {
        id: "s2",
        inputs: {
          prefill: {
            selectedParentComponent: "shell",
            selectedNozzleLocation: "shell",
          },
        },
      },
    } as any);
    render(
      <Api510ShellTubeCalculationWorkspace
        hasReportContext
        onCreateFindingDraft={onCreate}
        fieldValues={{
          "api510.external.exchanger.shell-tube.shell-side.design-pressure": 100,
          "api510.external.exchanger.shell-tube.shell-side.design-temperature": 100,
        }}
      />,
    );
    fireEvent.change(screen.getByLabelText("Component"), {
      target: { value: "nozzles" },
    });
    await mockResolveAllowableStress();
    fireEvent.click(screen.getByRole("button", { name: "Run Calculation" }));
    await screen.findByText(/Execution: success/i);
    fireEvent.click(
      screen.getByRole("button", { name: "Create Finding from Calculation" }),
    );
    expect(onCreate).toHaveBeenCalledWith(
      expect.objectContaining({
        parentComponent: "shell",
        nozzleLocation: "shell",
      }),
    );
  });

  it("failed calculation creates recommendation text", async () => {
    const onCreate = vi.fn();
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({
      success: false,
      calculationType: "ug-27-shell-tmin",
      componentKey: "shell",
      componentLabel: "Shell",
      pressureSide: "shell-side",
      inputsUsed: {},
      resultSummary: "failed",
      warnings: ["missing required context"],
    } as any);
    render(
      <Api510ShellTubeCalculationWorkspace
        onCreateFindingDraft={onCreate}
        fieldValues={{
          "api510.external.exchanger.shell-tube.shell-side.design-pressure": 100,
          "api510.external.exchanger.shell-tube.shell-side.design-temperature": 100,
        }}
      />,
    );
    await mockResolveAllowableStress();
    fireEvent.click(screen.getByRole("button", { name: "Run Calculation" }));
    await screen.findByText(/Execution: failure/i);
    fireEvent.click(
      screen.getByRole("button", { name: "Create Recommendation" }),
    );
    expect(onCreate).toHaveBeenCalledWith(
      expect.objectContaining({
        recommendationDraft: expect.stringContaining("Recommendation:"),
      }),
    );
  });

  it("standalone workspace still works without report context", async () => {
    vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({
      success: true,
      calculationType: "ug-27-shell-tmin",
      componentKey: "shell",
      componentLabel: "Shell",
      pressureSide: "shell-side",
      inputsUsed: {},
      resultSummary: "ok",
      warnings: [],
      snapshotReadyPayload: { id: "x" },
    } as any);
    render(
      <Api510ShellTubeCalculationWorkspace
        hasReportContext={false}
        fieldValues={{
          "api510.external.exchanger.shell-tube.shell-side.design-pressure": 100,
          "api510.external.exchanger.shell-tube.shell-side.design-temperature": 100,
        }}
      />,
    );
    await mockResolveAllowableStress();
    fireEvent.click(screen.getByRole("button", { name: "Run Calculation" }));
    expect(
      await screen.findByText(/Snapshot Preview: available/i),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("button", { name: "Save Calculation Snapshot" }),
    ).toBeDisabled();
  });

  it("standalone workspace does not show draft banner without draft setup", () => {
    render(<Api510ShellTubeCalculationWorkspace />);
    expect(
      screen.queryByText("Using draft setup from Start Wizard"),
    ).not.toBeInTheDocument();
    expect(
      screen.getByLabelText("Shell-Side Design Pressure"),
    ).toBeInTheDocument();
  });
});

it("standalone shell-side design conditions are passed via panel and manual override is hidden by default", async () => {
  vi.mocked(executeApi510ComponentCalculation).mockResolvedValueOnce({
    success: true,
    calculationType: "ug-27-shell-tmin",
    componentKey: "shell",
    componentLabel: "Shell",
    pressureSide: "shell-side",
    inputsUsed: {},
    resultSummary: "ok",
    warnings: [],
  } as any);
  render(<Api510ShellTubeCalculationWorkspace hasReportContext={false} />);
  expect(
    screen.queryByLabelText("Manual Allowable Stress (psi)"),
  ).not.toBeInTheDocument();
  fireEvent.change(screen.getByLabelText("Shell-Side Design Pressure"), {
    target: { value: "155" },
  });
  fireEvent.change(screen.getByLabelText("Shell-Side Design Temperature"), {
    target: { value: "450" },
  });
  await mockResolveAllowableStress();
  fireEvent.change(screen.getByLabelText("Joint Efficiency"), {
    target: { value: "1" },
  });
  fireEvent.change(screen.getByLabelText("Inside Diameter (in.)"), {
    target: { value: "48" },
  });
  fireEvent.change(screen.getByLabelText("Outside Diameter (in.)"), {
    target: { value: "48.5" },
  });
  fireEvent.change(screen.getByLabelText("Original/Nominal Thickness (in.)"), {
    target: { value: "0.5" },
  });
  fireEvent.change(
    screen.getByLabelText("Current / Provided Thickness (in.)"),
    { target: { value: "0.5" } },
  );
  fireEvent.change(screen.getByLabelText("Corrosion Allowance (in.)"), {
    target: { value: "0" },
  });
  await mockResolveAllowableStress();
  fireEvent.click(screen.getByRole("button", { name: "Run Calculation" }));
  expect(vi.mocked(executeApi510ComponentCalculation)).toHaveBeenCalledWith(
    expect.objectContaining({
      prefill: expect.objectContaining({
        designPressureValue: "155",
        designTemperatureValue: "450",
      }),
    }),
  );
});
