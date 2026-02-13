import { useQuery } from '@tanstack/react-query';
import { getDashboard } from '../api/projects';

export const useDashboard = (projectId: string) =>
  useQuery({
    queryKey: ['dashboard', projectId],
    queryFn: () => getDashboard(projectId),
    enabled: Boolean(projectId)
  });
