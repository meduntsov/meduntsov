export const ProjectScreen = () => (
  <section style={{ display: 'grid', gridTemplateColumns: '1fr 1fr 2fr', gap: 12 }}>
    <div><h3>WBS Structure</h3><ul><li>1.1 Design</li><li>2.1 Structure</li></ul></div>
    <div><h3>Physical Structure</h3><ul><li>Complex A / Building 1 / Floor 2 / Lot 2A</li></ul></div>
    <div><h3>Selected Element</h3><p>Asset registry links COST↔LOCATION↔SYSTEM.</p><h4>Assets</h4><ul><li>Structural frame - linked to WBS 2.1 and Lot 2A</li></ul></div>
  </section>
);
