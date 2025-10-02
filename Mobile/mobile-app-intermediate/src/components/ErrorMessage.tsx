import React from 'react';
import { HelperText } from 'react-native-paper';
export default function ErrorMessage({message}:{message?:string}){
  if(!message) return null;
  return <HelperText type="error" visible>{message}</HelperText>;
}
