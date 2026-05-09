import { Link } from 'react-router-dom';

type HomeCardProps = {
  title: string;
  description: string;
  links: Array<{ to: string; label: string }>;
};

function HomeCard({ title, description, links }: HomeCardProps) {
  return (
    <section className="card home-card" aria-label={title}>
      <h2>{title}</h2>
      <p className="muted">{description}</p>
      <div className="home-card-links">
        {links.map((link) => (
          <Link key={link.to} to={link.to} className="home-card-link">
            {link.label}
          </Link>
        ))}
      </div>
    </section>
  );
}

export function DashboardPage() {
  return (
    <div className="home-dashboard">
      <section className="card home-hero">
        <h2>Welcome</h2>
        <p className="muted">Use the sections below to access the primary product areas.</p>
      </section>

      <div className="home-card-grid">
        <HomeCard
          title="Engineering Tools"
          description="Calculators and reference tools that support engineering and inspection decisions."
          links={[
            { to: '/calculators/corrosion-rate', label: 'Corrosion Rate Calculator' },
            { to: '/calculators/pipe-lookup', label: 'Pipe Lookup Calculator' },
            { to: '/engineering-tools/criteria-references', label: 'Criteria / References' },
          ]}
        />
        <HomeCard
          title="API Inspection Reports"
          description="Access API inspection report queues, status slices, and report actions."
          links={[
            { to: '/reports', label: 'Open API Inspection Reports' },
            { to: '/reports/api-570-piping-external', label: 'Start API 570 Piping External Report' },
            { to: '/api-inspection-log', label: 'API Inspection Log' },
          ]}
        />
        <HomeCard
          title="NDE Log / Reports"
          description="Track NDE requests and reports separately from API inspection report workflows."
          links={[
            { to: '/nde-requests', label: 'NDE Requests' },
            { to: '/nde-reports', label: 'NDE Reports' },
            { to: '/schedule', label: 'Schedule' },
          ]}
        />
      </div>
    </div>
  );
}
