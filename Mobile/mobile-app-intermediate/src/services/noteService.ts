import { api } from './api';
export type Note = { id:string; title:string; content:string; createdAt:string };
export const NoteService = {
  list: async () => (await api.get('/notes')).data,
  get: async (id:string) => (await api.get(`/notes/${id}`)).data,
  create: async (payload: {title:string; content:string}) => (await api.post('/notes', payload)).data,
  update: async (id:string, payload: Partial<Note>) => (await api.put(`/notes/${id}`, payload)).data,
  remove: async (id:string) => (await api.delete(`/notes/${id}`)).data,
};
