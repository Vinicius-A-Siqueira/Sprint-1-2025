import React, {createContext, useEffect, useState} from 'react';
import { api } from '../services/api';
import AsyncStorage from '@react-native-async-storage/async-storage';

type User = { id: string; name: string; email: string };
type AuthCtx = {
  user: User | null;
  loading: boolean;
  signIn: (email:string, password:string)=>Promise<void>;
  signUp: (name:string, email:string, password:string)=>Promise<void>;
  signOut: ()=>Promise<void>;
};
export const AuthContext = createContext<AuthCtx>({} as any);

export function AuthProvider({children}:{children:React.ReactNode}) {
  const [user, setUser] = useState<User|null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => { (async () => {
    const token = await AsyncStorage.getItem('token');
    const u = await AsyncStorage.getItem('user');
    if (token) api.defaults.headers.common.Authorization = `Bearer ${token}`;
    if (u) setUser(JSON.parse(u));
    setLoading(false);
  })(); }, []);

  const signIn = async (email:string, password:string) => {
    setLoading(true);
    try {
      const { data } = await api.post('/auth/login', { email, password });
      api.defaults.headers.common.Authorization = `Bearer ${data.token}`;
      await AsyncStorage.multiSet([['token', data.token], ['user', JSON.stringify(data.user)]]);
      setUser(data.user);
    } finally { setLoading(false); }
  };

  const signUp = async (name:string, email:string, password:string) => {
    setLoading(true);
    try {
      await api.post('/auth/register', { name, email, password });
      await signIn(email, password);
    } finally { setLoading(false); }
  };

  const signOut = async () => {
    await AsyncStorage.multiRemove(['token','user']);
    setUser(null);
    // @ts-ignore
    delete api.defaults.headers.common.Authorization;
  };

  return <AuthContext.Provider value={{ user, loading, signIn, signUp, signOut }}>{children}</AuthContext.Provider>;
}
