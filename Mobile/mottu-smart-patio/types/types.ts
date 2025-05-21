export interface Moto {
  id: string;
  placa: string;
  status: string;
  imagem: string;
}

export type RootTabParamList = {
  Início: undefined;
  'Lista de Motos': undefined;       
  'Cadastrar Moto': { motoToEdit?: Moto };  
  Configurações: undefined;
};

export type RootStackParamList = {
  Tabs:
    | { screen: 'Cadastrar Moto'; params?: { motoToEdit?: Moto } }
    | { screen: Exclude<keyof RootTabParamList, 'Cadastrar Moto'>; params?: undefined }
    | undefined;
  'Detalhes da Moto': { moto: Moto };
  'Lista de Motos': undefined;
};