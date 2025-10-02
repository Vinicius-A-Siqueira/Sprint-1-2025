import React, {createContext, useContext, useEffect, useState} from 'react';
import { MD3DarkTheme as DarkTheme, MD3LightTheme as LightTheme, PaperProvider } from 'react-native-paper';
import AsyncStorage from '@react-native-async-storage/async-storage';

type ThemeCtx = { isDark: boolean; toggle: () => void };
const ThemeContext = createContext<ThemeCtx>({ isDark:false, toggle: () => {} });
export const useThemeCtx = () => useContext(ThemeContext);

export function ThemeProvider({children}:{children:React.ReactNode}) {
  const [isDark, setIsDark] = useState(false);
  useEffect(() => { AsyncStorage.getItem('theme').then(v => setIsDark(v==='dark')); }, []);
  const toggle = () => {
    setIsDark(prev => {
      const next = !prev;
      AsyncStorage.setItem('theme', next ? 'dark' : 'light');
      return next;
    });
  };
  const theme = isDark ? DarkTheme : LightTheme;
  return (
    <ThemeContext.Provider value={{isDark, toggle}}>
      <PaperProvider theme={theme}>{children}</PaperProvider>
    </ThemeContext.Provider>
  );
}
