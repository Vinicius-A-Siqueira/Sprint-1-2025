import { api } from './api';
export type Task = { id:string; title:string; done:boolean; createdAt:string };
export const TaskService = {
  list: async () => (await api.get('/tasks')).data,
  get: async (id:string) => (await api.get(`/tasks/${id}`)).data,
  create: async (payload: {title:string; done:boolean}) => (await api.post('/tasks', payload)).data,
  update: async (id:string, payload: Partial<Task>) => (await api.put(`/tasks/${id}`, payload)).data,
  remove: async (id:string) => (await api.delete(`/tasks/${id}`)).data,
};
