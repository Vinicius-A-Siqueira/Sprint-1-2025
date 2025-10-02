import React from 'react';
import { FlatList, RefreshControl } from 'react-native';
import { ActivityIndicator, FAB, List, HelperText } from 'react-native-paper';
import { NoteService, Note } from '../../services/noteService';

export default function NoteListScreen({navigation}:any){
  const [data, setData] = React.useState<Note[]>([]);
  const [loading, setLoading] = React.useState(true);
  const [error, setError] = React.useState<string|undefined>();

  const load = async () => {
    setLoading(true);
    setError(undefined);
    try { setData(await NoteService.list()); }
    catch(e:any){ setError(e?.response?.data?.message || 'Falha ao carregar'); }
    finally{ setLoading(false); }
  };

  React.useEffect(()=>{ load(); }, []);

  if (loading) return <ActivityIndicator style={{ marginTop: 32 }} />;
  return (
    <>
      {error ? <HelperText type="error" visible>{error}</HelperText> : null}
      <FlatList
        data={data}
        keyExtractor={i=>i.id}
        renderItem={({item})=>(
          <List.Item
            title={item.title}
            description={item.content?.slice(0,60)}
            onPress={()=>navigation.navigate('NoteDetail', { id: item.id })}
            right={props => <List.Icon {...props} icon={'note-text'} />}
          />
        )}
        refreshControl={<RefreshControl refreshing={loading} onRefresh={load} />}
      />
      <FAB icon="plus" style={{ position:'absolute', right:16, bottom:16 }} onPress={()=>navigation.navigate('NoteForm')} />
    </>
  );
}
