import AsyncStorage from '@react-native-async-storage/async-storage';
import React, { useEffect, useState } from 'react';
import { Alert, Button, StyleSheet, Text, TextInput, View } from 'react-native';

export default function RegisterMotoScreen() {
    const [placa, setPlaca] = useState('');
    const [modelo, setModelo] = useState('');

    const salvar = async () => {
        if (!placa || !modelo) return Alert.alert('Erro', 'Preencha todos os campos');

        const dados = { placa, modelo };
        await AsyncStorage.setItem('ultimaMoto', JSON.stringify(dados));
        Alert.alert('Sucesso', 'Moto cadastrada com sucesso!');
    };

    const carregar = async () => {
        const salvo = await AsyncStorage.getItem('ultimaMoto');
        if (salvo) {
            const { placa, modelo } = JSON.parse(salvo);
            setPlaca(placa);
            setModelo(modelo);
        }
    };

    useEffect(() => {
        carregar();
    }, []);

    return (
        <View style={styles.container}>
            <Text style={styles.label}>Placa:</Text>
            <TextInput style={styles.input} value={placa} onChangeText={setPlaca} />
            <Text style={styles.label}>Modelo:</Text>
            <TextInput style={styles.input} value={modelo} onChangeText={setModelo} />
            <Button title="Salvar Moto" onPress={salvar} />
        </View>
    );
}

const styles = StyleSheet.create({
    container: { flex: 1, padding: 20 },
    label: { fontWeight: 'bold', marginTop: 10 },
    input: { borderWidth: 1, borderColor: '#ccc', padding: 8, borderRadius: 4 }
});
