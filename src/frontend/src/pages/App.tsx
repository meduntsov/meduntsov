import { Dashboard } from '../components/Dashboard';
import { ProjectScreen } from '../components/ProjectScreen';
import { AiPanel } from '../components/AiPanel';
import { useDashboard } from '../hooks/useDashboard';

const DEMO_PROJECT_ID = import.meta.env.VITE_DEMO_PROJECT_ID ?? '00000000-0000-0000-0000-000000000001';

export const App = () => {
  const { data, isLoading, error } = useDashboard(DEMO_PROJECT_ID);

  return (
    <main style={{ fontFamily: 'sans-serif', padding: 16, display: 'grid', gap: 16 }}>
      <h1>Developer ERP MVP (AI-ready)</h1>
      <ProjectScreen />
      {isLoading && <p>Loading dashboard...</p>}
      {error && <p>Failed to load dashboard.</p>}
      {data && (
        <>
          <Dashboard data={data} />
          <AiPanel projectId={DEMO_PROJECT_ID} data={data} />
        </>
      )}
    </main>
  );
};
