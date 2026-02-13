export type ReadinessSlice = { dimension: string; key: string; financialReadiness: number; technicalReadiness: number };
export type ReadinessResult = { financialReadiness: number; technicalReadiness: number; drillDown: ReadinessSlice[] };
export type SimilarProject = { projectId: string; projectCode: string; similarityScore: number; explanation: string };
export type RiskPrediction = { milestone: string; riskScore: number; drivers: string[] };
export type DashboardResponse = { id: string; code: string; readiness: ReadinessResult; risk: RiskPrediction[]; similar: SimilarProject[] };
export type RagCitation = { projectCode: string; document: string; snippet: string; similarity: number };
export type RagAnswer = { answer: string; citations: RagCitation[] };
