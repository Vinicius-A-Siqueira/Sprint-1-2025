import React from 'react';
import { StyleSheet, Switch, Text, View } from 'react-native';

export default function SettingsScreen() {
    const [notificacoes, setNotificacoes] = React.useState(true);

    return (
        <View style={styles.container}>
            <Text style={styles.title}>⚙️ Configurações</Text>
            <View style={styles.setting}>
                <Text>Notificações</Text>
                <Switch value={notificacoes} onValueChange={setNotificacoes} />
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    container: { flex: 1, padding: 20 },
    title: { fontSize: 22, marginBottom: 20 },
    setting: { flexDirection: 'row', justifyContent: 'space-between', alignItems: 'center' }
});
