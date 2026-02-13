import type { DashboardResponse } from '../types/api';

export const Dashboard = ({ data }: { data: DashboardResponse }) => (
  <section>
    <h3>Readiness Dashboard</h3>
    <p>Financial readiness: {(data.readiness.financialReadiness * 100).toFixed(1)}%</p>
    <p>Technical readiness: {(data.readiness.technicalReadiness * 100).toFixed(1)}%</p>
    <h4>Drill-down</h4>
    <ul>
      {data.readiness.drillDown.map((slice) => (
        <li key={slice.key}>{slice.dimension}/{slice.key}: F {Math.round(slice.financialReadiness * 100)}% · T {Math.round(slice.technicalReadiness * 100)}%</li>
      ))}
    </ul>
  </section>
);
