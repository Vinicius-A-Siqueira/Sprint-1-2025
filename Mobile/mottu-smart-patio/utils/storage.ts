import AsyncStorage from '@react-native-async-storage/async-storage';
import { Moto } from '../types/types';

const STORAGE_KEY = '@motos';

export async function saveMoto(moto: Moto): Promise<void> {
  try {
    const existing = await getMotos();
    const exists = existing.some(m => m.id === moto.id);
    if (!exists) {
      const updated = [...existing, moto];
      await AsyncStorage.setItem(STORAGE_KEY, JSON.stringify(updated));
    } else {
      console.log('Moto com esse ID já existe, não será adicionada.');
    }
  } catch (error) {
    console.error('Erro ao salvar moto:', error);
  }
}

export async function getMotos(): Promise<Moto[]> {
  try {
    const data = await AsyncStorage.getItem(STORAGE_KEY);
    return data ? JSON.parse(data) : [];
  } catch (error) {
    console.error('Erro ao carregar motos:', error);
    return [];
  }
}

export async function updateMoto(updatedMoto: Moto): Promise<void> {
  try {
    const motos = await getMotos();
    const filtered = motos.filter(m => m.id !== updatedMoto.id);
    filtered.push(updatedMoto);
    await AsyncStorage.setItem(STORAGE_KEY, JSON.stringify(filtered));
  } catch (error) {
    console.error('Erro ao atualizar moto:', error);
  }
}

export async function deleteMoto(id: string): Promise<void> {
  try {
    const motos = await getMotos();
    const filtered = motos.filter(m => m.id !== id);
    await AsyncStorage.setItem(STORAGE_KEY, JSON.stringify(filtered));
  } catch (error) {
    console.error('Erro ao deletar moto:', error);
  }
}
