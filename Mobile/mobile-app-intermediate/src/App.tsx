import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import AuthRoutes from './routes/AuthRoutes';
import AppRoutes from './routes/AppRoutes';
import { AuthProvider, AuthContext } from './contexts/AuthContext';
import { ThemeProvider } from './theme';

export default function App(){
  return (
    <ThemeProvider>
      <AuthProvider>
        <AuthContext.Consumer>
          {({user, loading}:any)=>(
            <NavigationContainer>
              {loading ? null : user ? <AppRoutes/> : <AuthRoutes/>}
            </NavigationContainer>
          )}
        </AuthContext.Consumer>
      </AuthProvider>
    </ThemeProvider>
  );
}
