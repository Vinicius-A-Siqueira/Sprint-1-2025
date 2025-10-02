import React from 'react';
import { IconButton } from 'react-native-paper';
import { useThemeCtx } from '../theme';

export default function ThemeToggle(){
  const { isDark, toggle } = useThemeCtx();
  return <IconButton icon={isDark ? 'weather-sunny' : 'moon-waning-crescent'} onPress={toggle} accessibilityLabel="Alternar tema" />;
}
