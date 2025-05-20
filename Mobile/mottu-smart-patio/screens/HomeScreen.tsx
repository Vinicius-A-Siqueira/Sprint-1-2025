import { NativeStackScreenProps } from '@react-navigation/native-stack';
import React from 'react';
import { Button, StyleSheet, Text, View } from 'react-native';
import { RootStackParamList } from '../App';

type Props = NativeStackScreenProps<RootStackParamList, 'Início'>;

export default function HomeScreen({ navigation }: Props) {
    return (
        <View style={styles.container}>
            <Text style={styles.title}>🏍️ Mottu Smart Pátio</Text>
            <Button title="Mapa do Pátio" onPress={() => navigation.navigate('Mapa do Pátio')} />
            <Button title="Cadastrar Moto" onPress={() => navigation.navigate('Cadastrar Moto')} />
            <Button title="Detalhes da Moto" onPress={() => navigation.navigate('Detalhes da Moto')} />
            <Button title="Configurações" onPress={() => navigation.navigate('Configurações')} />
        </View>
    );
}

const styles = StyleSheet.create({
    container: { flex: 1, justifyContent: 'center', padding: 20, gap: 15 },
    title: { fontSize: 24, textAlign: 'center', marginBottom: 20 }
});
