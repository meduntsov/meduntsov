import { useState } from 'react';
import { askAi } from '../api/projects';
import type { DashboardResponse, RagAnswer } from '../types/api';

export const AiPanel = ({ projectId, data }: { projectId: string; data: DashboardResponse }) => {
  const [question, setQuestion] = useState('What risks from similar projects should we track?');
  const [answer, setAnswer] = useState<RagAnswer | null>(null);

  const onAsk = async () => setAnswer(await askAi(projectId, question));

  return (
    <section>
      <h3>AI Panel</h3>
      <h4>Similar projects</h4>
      <ul>{data.similar.map((x) => <li key={x.projectId}>{x.projectCode} ({x.similarityScore.toFixed(2)}): {x.explanation}</li>)}</ul>
      <h4>Risk explanation</h4>
      <ul>{data.risk.map((x) => <li key={x.milestone}>{x.milestone}: {(x.riskScore * 100).toFixed(0)}% ({x.drivers.join(', ')})</li>)}</ul>
      <h4>Ask AI (RAG)</h4>
      <input value={question} onChange={(e) => setQuestion(e.target.value)} style={{ width: '100%' }} />
      <button onClick={onAsk}>Ask</button>
      {answer && (
        <div>
          <p>{answer.answer}</p>
          <ul>{answer.citations.map((c, i) => <li key={i}>{c.document}: {c.snippet}</li>)}</ul>
        </div>
      )}
    </section>
  );
};
