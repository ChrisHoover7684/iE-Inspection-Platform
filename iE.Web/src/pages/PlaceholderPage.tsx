type PlaceholderPageProps = {
  title: string;
  description: string;
};

export function PlaceholderPage({ title, description }: PlaceholderPageProps) {
  return (
    <section className="placeholder-page card">
      <p className="placeholder-eyebrow">Planned Experience</p>
      <h2>{title}</h2>
      <p>{description}</p>
      <p className="muted">This area is reserved for future improvements in this workspace.</p>
    </section>
  );
}
