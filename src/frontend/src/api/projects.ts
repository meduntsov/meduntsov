import { apiClient } from './client';
import type { DashboardResponse, RagAnswer } from '../types/api';

export const getDashboard = async (projectId: string) => {
  const { data } = await apiClient.get<DashboardResponse>(`/projects/${projectId}/dashboard`);
  return data;
};

export const askAi = async (projectId: string, question: string) => {
  const { data } = await apiClient.post<RagAnswer>(`/projects/${projectId}/ask-ai`, { question });
  return data;
};
