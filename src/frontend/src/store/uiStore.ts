import { create } from 'zustand';

type UiState = {
  projectId: string;
  selectedWbs: string | null;
  selectedLocation: string | null;
  setProjectId: (id: string) => void;
};

export const useUiStore = create<UiState>((set) => ({
  projectId: '',
  selectedWbs: null,
  selectedLocation: null,
  setProjectId: (projectId) => set({ projectId })
}));
