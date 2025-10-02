import axios from 'axios';
export const api = axios.create({
  baseURL: process.env.EXPO_PUBLIC_API_URL || 'http://10.0.2.2:8080',
  timeout: 10000,
});
