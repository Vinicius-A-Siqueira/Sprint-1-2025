SET SERVEROUTPUT ON;

DROP TABLE camera CASCADE CONSTRAINTS;
DROP TABLE deteccao CASCADE CONSTRAINTS;
DROP TABLE filial CASCADE CONSTRAINTS;
DROP TABLE leitura CASCADE CONSTRAINTS;
DROP TABLE manutencao CASCADE CONSTRAINTS;
DROP TABLE moto CASCADE CONSTRAINTS;
DROP TABLE patio CASCADE CONSTRAINTS;
DROP TABLE relation_7 CASCADE CONSTRAINTS;
DROP TABLE sensor_iot CASCADE CONSTRAINTS;
DROP TABLE statusmoto CASCADE CONSTRAINTS;
DROP TABLE usuario CASCADE CONSTRAINTS;

SELECT * FROM filial;
SELECT * FROM statusmoto;
SELECT * FROM patio;
SELECT * FROM sensor_iot;
SELECT * FROM moto;
SELECT * FROM leitura;
SELECT * FROM camera;
SELECT * FROM deteccao;
SELECT * FROM manutencao;
SELECT * FROM usuario;
SELECT * FROM relation_7;

CREATE TABLE filial (
    id_filial NUMBER(9) NOT NULL,
    nome      VARCHAR2(100) NOT NULL,
    endereco  VARCHAR2(200) NOT NULL,
    CONSTRAINT filial_pk PRIMARY KEY (id_filial)
);

INSERT INTO filial (id_filial, nome, endereco) VALUES
(1, 'Matriz São Paulo', 'Rua Augusta, 1234 - São Paulo, SP');
INSERT INTO filial (id_filial, nome, endereco) VALUES
(2, 'Filial Rio de Janeiro', 'Avenida Atlântica, 567 - Rio de Janeiro, RJ');
INSERT INTO filial (id_filial, nome, endereco) VALUES
(3, 'Filial Belo Horizonte', 'Avenida Afonso Pena, 890 - Belo Horizonte, MG');
INSERT INTO filial (id_filial, nome, endereco) VALUES
(4, 'Filial Porto Alegre', 'Rua dos Andradas, 1011 - Porto Alegre, RS');
INSERT INTO filial (id_filial, nome, endereco) VALUES
(5, 'Filial Curitiba', 'Avenida Paraná, 1213 - Curitiba, PR');
INSERT INTO filial (id_filial, nome, endereco) VALUES
(6, 'Filial Salvador', 'Avenida Sete de Setembro, 1415 - Salvador, BA');
INSERT INTO filial (id_filial, nome, endereco) VALUES
(7, 'Filial Brasília', 'Setor Comercial Sul, Quadra 6, Bloco A - Brasília, DF');
INSERT INTO filial (id_filial, nome, endereco) VALUES
(8, 'Filial Manaus', 'Avenida Amazonas, 1617 - Manaus, AM');
INSERT INTO filial (id_filial, nome, endereco) VALUES
(9, 'Filial Recife', 'Avenida Boa Viagem, 1819 - Recife, PE');
INSERT INTO filial (id_filial, nome, endereco) VALUES
(10, 'Filial Campinas', 'Avenida Brasil, 2021 - Campinas, SP');

CREATE TABLE statusmoto (
    id_status NUMBER(9) NOT NULL,
    descricao VARCHAR2(50) NOT NULL,
    CONSTRAINT statusmoto_pk PRIMARY KEY (id_status)
);

INSERT INTO statusmoto (id_status, descricao) VALUES
(1, 'Disponível');
INSERT INTO statusmoto (id_status, descricao) VALUES
(2, 'Em Manutenção');
INSERT INTO statusmoto (id_status, descricao) VALUES
(3, 'Indisponível');
INSERT INTO statusmoto (id_status, descricao) VALUES
(4, 'Em Uso');
INSERT INTO statusmoto (id_status, descricao) VALUES
(5, 'Aguardando Peças');
INSERT INTO statusmoto (id_status, descricao) VALUES
(6, 'Baixada');
INSERT INTO statusmoto (id_status, descricao) VALUES
(7, 'Reservada');
INSERT INTO statusmoto (id_status, descricao) VALUES
(8, 'Em Teste');
INSERT INTO statusmoto (id_status, descricao) VALUES
(9, 'Liberada');
INSERT INTO statusmoto (id_status, descricao) VALUES
(10, 'Bloqueada');

CREATE TABLE patio (
    id_patio         NUMBER(9) NOT NULL,
    nome             VARCHAR2(100) NOT NULL,
    localizacao      VARCHAR2(200) NOT NULL,
    largura          NUMBER(10, 2) NOT NULL,
    comprimento      NUMBER(10, 2) NOT NULL,
    area_total       NUMBER(10, 2) NOT NULL,
    filial_id_filial NUMBER(9) NOT NULL,
    CONSTRAINT patio_pk PRIMARY KEY (id_patio, filial_id_filial),
    CONSTRAINT patio_filial_fk FOREIGN KEY (filial_id_filial)
        REFERENCES filial (id_filial)
);

INSERT INTO patio (id_patio, nome, localizacao, largura, comprimento, area_total, filial_id_filial) VALUES
(1, 'Pátio A - SP', 'Rua Lateral, 10', 25.5, 40.0, 1020.00, 1);
INSERT INTO patio (id_patio, nome, localizacao, largura, comprimento, area_total, filial_id_filial) VALUES
(2, 'Pátio B - SP', 'Avenida Principal, 200', 30.0, 50.0, 1500.00, 1);
INSERT INTO patio (id_patio, nome, localizacao, largura, comprimento, area_total, filial_id_filial) VALUES
(3, 'Pátio Centro - RJ', 'Rua da Praia, 30', 20.0, 35.0, 700.00, 2);
INSERT INTO patio (id_patio, nome, localizacao, largura, comprimento, area_total, filial_id_filial) VALUES
(4, 'Pátio Norte - BH', 'Rodovia BR-040, Km 50', 40.0, 60.0, 2400.00, 3);
INSERT INTO patio (id_patio, nome, localizacao, largura, comprimento, area_total, filial_id_filial) VALUES
(5, 'Pátio Sul - POA', 'Avenida Ipiranga, 1500', 15.0, 30.0, 450.00, 4);
INSERT INTO patio (id_patio, nome, localizacao, largura, comprimento, area_total, filial_id_filial) VALUES
(6, 'Pátio Leste - CTB', 'Rua das Flores, 20', 22.0, 45.0, 990.00, 5);
INSERT INTO patio (id_patio, nome, localizacao, largura, comprimento, area_total, filial_id_filial) VALUES
(7, 'Pátio Oeste - SSA', 'Avenida Contorno, 400', 28.0, 55.0, 1540.00, 6);
INSERT INTO patio (id_patio, nome, localizacao, largura, comprimento, area_total, filial_id_filial) VALUES
(8, 'Pátio Central - BSB', 'Eixo Monumental, S/N', 35.0, 70.0, 2450.00, 7);
INSERT INTO patio (id_patio, nome, localizacao, largura, comprimento, area_total, filial_id_filial) VALUES
(9, 'Pátio Industrial - MAO', 'Distrito Industrial, Quadra 5', 50.0, 80.0, 4000.00, 8);
INSERT INTO patio (id_patio, nome, localizacao, largura, comprimento, area_total, filial_id_filial) VALUES
(10, 'Pátio Boa Viagem - REC', 'Avenida Beira Mar, 60', 18.0, 32.0, 576.00, 9);


CREATE TABLE sensor_iot (
    id_sensor    NUMBER(9) NOT NULL,
    tipo         VARCHAR2(50) NOT NULL,
    fabricante   VARCHAR2(100) NOT NULL,
    CONSTRAINT sensor_iot_pk PRIMARY KEY (id_sensor)
);

INSERT INTO sensor_iot (id_sensor, tipo, fabricante) VALUES (1, 'GPS', 'SensorTech');
INSERT INTO sensor_iot (id_sensor, tipo, fabricante) VALUES (2, 'Temperatura', 'ClimaSens');
INSERT INTO sensor_iot (id_sensor, tipo, fabricante) VALUES (3, 'Umidade', 'ClimaSens');
INSERT INTO sensor_iot (id_sensor, tipo, fabricante) VALUES (4, 'Pressão', 'SensorTech');
INSERT INTO sensor_iot (id_sensor, tipo, fabricante) VALUES (5, 'Proximidade', 'ProxiTech');
INSERT INTO sensor_iot (id_sensor, tipo, fabricante) VALUES (6, 'Câmera', 'VisionTech');
INSERT INTO sensor_iot (id_sensor, tipo, fabricante) VALUES (7, 'Temperatura', 'HeatWave');
INSERT INTO sensor_iot (id_sensor, tipo, fabricante) VALUES (8, 'GPS', 'TrackMaster');
INSERT INTO sensor_iot (id_sensor, tipo, fabricante) VALUES (9, 'Proximidade', 'ProxiTech');
INSERT INTO sensor_iot (id_sensor, tipo, fabricante) VALUES (10, 'Umidade', 'ClimaSens');

CREATE TABLE moto (
    id_moto                NUMBER(9) NOT NULL,
    placa                  VARCHAR2(10),
    ano_fabricacao         DATE NOT NULL,
    sensor_iot_id_sensor   NUMBER(9) NOT NULL,
    statusmoto_id_status   NUMBER(9),
    patio_id_patio         NUMBER(9) NOT NULL,
    patio_filial_id_filial NUMBER(9) NOT NULL,
    CONSTRAINT moto_pk PRIMARY KEY (id_moto),
    CONSTRAINT moto_placa_un UNIQUE (placa),
    CONSTRAINT moto_sensor_iot_fk FOREIGN KEY (sensor_iot_id_sensor)
        REFERENCES sensor_iot (id_sensor),
    CONSTRAINT moto_statusmoto_fk FOREIGN KEY (statusmoto_id_status)
        REFERENCES statusmoto (id_status),
    CONSTRAINT moto_patio_fk FOREIGN KEY (patio_id_patio, patio_filial_id_filial)
        REFERENCES patio (id_patio, filial_id_filial)
);

ALTER TABLE moto ADD status VARCHAR2(255 CHAR) DEFAULT 'ATIVO' NOT NULL;

INSERT INTO moto (id_moto, placa, ano_fabricacao, sensor_iot_id_sensor, statusmoto_id_status, patio_id_patio, patio_filial_id_filial) 
VALUES (1, 'ABC1234', TO_DATE('2021-01-10', 'YYYY-MM-DD'), 1, 1, 1, 1);
INSERT INTO moto (id_moto, placa, ano_fabricacao, sensor_iot_id_sensor, statusmoto_id_status, patio_id_patio, patio_filial_id_filial) 
VALUES (2, 'DEF5678', TO_DATE('2022-03-15', 'YYYY-MM-DD'), 2, 2, 1, 1);
INSERT INTO moto (id_moto, placa, ano_fabricacao, sensor_iot_id_sensor, statusmoto_id_status, patio_id_patio, patio_filial_id_filial) 
VALUES (3, 'GHI9012', TO_DATE('2020-07-25', 'YYYY-MM-DD'), 3, 3, 2, 2);
INSERT INTO moto (id_moto, placa, ano_fabricacao, sensor_iot_id_sensor, statusmoto_id_status, patio_id_patio, patio_filial_id_filial) 
VALUES (4, 'JKL3456', TO_DATE('2019-10-18', 'YYYY-MM-DD'), 4, 1, 3, 3);
INSERT INTO moto (id_moto, placa, ano_fabricacao, sensor_iot_id_sensor, statusmoto_id_status, patio_id_patio, patio_filial_id_filial) 
VALUES (5, 'QRS8765', TO_DATE('2017-11-12', 'YYYY-MM-DD'), 5, 2, 3, 2);
INSERT INTO moto (id_moto, placa, ano_fabricacao, sensor_iot_id_sensor, statusmoto_id_status, patio_id_patio, patio_filial_id_filial) 
VALUES (6, 'PQR1234', TO_DATE('2022-07-23', 'YYYY-MM-DD'), 6, 1, 3, 2);
INSERT INTO moto (id_moto, placa, ano_fabricacao, sensor_iot_id_sensor, statusmoto_id_status, patio_id_patio, patio_filial_id_filial) 
VALUES (7, 'TUV4567', TO_DATE('2020-02-18', 'YYYY-MM-DD'), 7, 3, 4, 3);
INSERT INTO moto (id_moto, placa, ano_fabricacao, sensor_iot_id_sensor, statusmoto_id_status, patio_id_patio, patio_filial_id_filial) 
VALUES (8, 'STU9876', TO_DATE('2018-05-10', 'YYYY-MM-DD'), 8, 2, 4, 3);
INSERT INTO moto (id_moto, placa, ano_fabricacao, sensor_iot_id_sensor, statusmoto_id_status, patio_id_patio, patio_filial_id_filial) 
VALUES (9, 'DEF1234', TO_DATE('2021-12-01', 'YYYY-MM-DD'), 9, 1, 5, 4);
INSERT INTO moto (id_moto, placa, ano_fabricacao, sensor_iot_id_sensor, statusmoto_id_status, patio_id_patio, patio_filial_id_filial) 
VALUES (10, 'GHI5678', TO_DATE('2023-08-30', 'YYYY-MM-DD'), 10, 3, 5, 4);

CREATE TABLE leitura (
    id_leitura NUMBER(9) NOT NULL,
    data_hora  TIMESTAMP(0) NOT NULL,
    CONSTRAINT leitura_pk PRIMARY KEY (id_leitura)
);

INSERT INTO leitura (id_leitura, data_hora) 
VALUES (1, TO_TIMESTAMP('2025-05-01 08:30:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO leitura (id_leitura, data_hora) 
VALUES (2, TO_TIMESTAMP('2025-05-02 09:45:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO leitura (id_leitura, data_hora) 
VALUES (3, TO_TIMESTAMP('2025-05-03 10:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO leitura (id_leitura, data_hora) 
VALUES (4, TO_TIMESTAMP('2025-05-04 11:15:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO leitura (id_leitura, data_hora) 
VALUES (5, TO_TIMESTAMP('2025-05-05 12:30:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO leitura (id_leitura, data_hora) 
VALUES (6, TO_TIMESTAMP('2025-05-06 13:45:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO leitura (id_leitura, data_hora) 
VALUES (7, TO_TIMESTAMP('2025-05-07 14:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO leitura (id_leitura, data_hora) 
VALUES (8, TO_TIMESTAMP('2025-05-08 15:30:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO leitura (id_leitura, data_hora) 
VALUES (9, TO_TIMESTAMP('2025-05-09 16:45:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO leitura (id_leitura, data_hora) 
VALUES (10, TO_TIMESTAMP('2025-05-10 17:00:00', 'YYYY-MM-DD HH24:MI:SS'));


CREATE TABLE camera (
    id_camera       NUMBER(9) NOT NULL,
    localizacao     VARCHAR2(200) NOT NULL,
    modelo          VARCHAR2(100) NOT NULL,
    patio_id_patio  NUMBER(9) NOT NULL,
    patio_id_filial NUMBER(9) NOT NULL,
    CONSTRAINT camera_pk PRIMARY KEY (id_camera, patio_id_patio, patio_id_filial),
    CONSTRAINT camera_patio_fk FOREIGN KEY (patio_id_patio, patio_id_filial)
        REFERENCES patio (id_patio, filial_id_filial)
);

INSERT INTO camera (id_camera, localizacao, modelo, patio_id_patio, patio_id_filial) 
VALUES (1, 'Pátio A - SP - Zona Norte', 'Modelo X100', 1, 1);
INSERT INTO camera (id_camera, localizacao, modelo, patio_id_patio, patio_id_filial) 
VALUES (2, 'Pátio B - SP - Zona Sul', 'Modelo X200', 2, 1);
INSERT INTO camera (id_camera, localizacao, modelo, patio_id_patio, patio_id_filial) 
VALUES (3, 'Pátio Centro - RJ - Centro', 'Modelo X300', 3, 2);
INSERT INTO camera (id_camera, localizacao, modelo, patio_id_patio, patio_id_filial) 
VALUES (4, 'Pátio Norte - BH - Rodovia', 'Modelo X400', 4, 3);
INSERT INTO camera (id_camera, localizacao, modelo, patio_id_patio, patio_id_filial) 
VALUES (5, 'Pátio Sul - POA - Bairro Ipiranga', 'Modelo X500', 5, 4);
INSERT INTO camera (id_camera, localizacao, modelo, patio_id_patio, patio_id_filial) 
VALUES (6, 'Pátio Leste - CTB - Rua das Flores', 'Modelo X600', 6, 5);
INSERT INTO camera (id_camera, localizacao, modelo, patio_id_patio, patio_id_filial) 
VALUES (7, 'Pátio Oeste - SSA - Avenida Contorno', 'Modelo X700', 7, 6);
INSERT INTO camera (id_camera, localizacao, modelo, patio_id_patio, patio_id_filial) 
VALUES (8, 'Pátio Central - BSB - Eixo Monumental', 'Modelo X800', 8, 7);
INSERT INTO camera (id_camera, localizacao, modelo, patio_id_patio, patio_id_filial) 
VALUES (9, 'Pátio Industrial - MAO - Distrito Industrial', 'Modelo X900', 9, 8);
INSERT INTO camera (id_camera, localizacao, modelo, patio_id_patio, patio_id_filial) 
VALUES (10, 'Pátio Boa Viagem - REC - Avenida Beira Mar', 'Modelo X1000', 10, 9);

CREATE TABLE deteccao (
    id_deteccao      NUMBER(9) NOT NULL,
    data_hora        TIMESTAMP(0) NOT NULL,
    coordenadas      VARCHAR2(100) NOT NULL,
    moto_id_moto     NUMBER(9) NOT NULL,
    camera_id_camera NUMBER(9) NOT NULL,
    camera_id_patio  NUMBER(9) NOT NULL,
    camera_id_filial NUMBER(9) NOT NULL,
    CONSTRAINT deteccao_pk PRIMARY KEY (id_deteccao, moto_id_moto),
    CONSTRAINT deteccao_camera_fk FOREIGN KEY (camera_id_camera, camera_id_patio, camera_id_filial)
        REFERENCES camera (id_camera, patio_id_patio, patio_id_filial),
    CONSTRAINT deteccao_moto_fk FOREIGN KEY (moto_id_moto)
        REFERENCES moto (id_moto)
);

INSERT INTO deteccao (id_deteccao, data_hora, coordenadas, moto_id_moto, camera_id_camera, camera_id_patio, camera_id_filial) 
VALUES (1, TO_TIMESTAMP('2025-05-13 08:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'Lat: -23.5505, Long: -46.6333', 1, 1, 1, 1);
INSERT INTO deteccao (id_deteccao, data_hora, coordenadas, moto_id_moto, camera_id_camera, camera_id_patio, camera_id_filial) 
VALUES (2, TO_TIMESTAMP('2025-05-13 08:15:00', 'YYYY-MM-DD HH24:MI:SS'), 'Lat: -23.5506, Long: -46.6334', 2, 2, 2, 1);
INSERT INTO deteccao (id_deteccao, data_hora, coordenadas, moto_id_moto, camera_id_camera, camera_id_patio, camera_id_filial) 
VALUES (3, TO_TIMESTAMP('2025-05-13 08:30:00', 'YYYY-MM-DD HH24:MI:SS'), 'Lat: -22.9068, Long: -43.1729', 3, 3, 3, 2);
INSERT INTO deteccao (id_deteccao, data_hora, coordenadas, moto_id_moto, camera_id_camera, camera_id_patio, camera_id_filial) 
VALUES (4, TO_TIMESTAMP('2025-05-13 08:45:00', 'YYYY-MM-DD HH24:MI:SS'), 'Lat: -19.9167, Long: -43.9345', 4, 4, 4, 3);
INSERT INTO deteccao (id_deteccao, data_hora, coordenadas, moto_id_moto, camera_id_camera, camera_id_patio, camera_id_filial) 
VALUES (5, TO_TIMESTAMP('2025-05-13 09:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'Lat: -30.0346, Long: -51.2177', 5, 5, 5, 4);
INSERT INTO deteccao (id_deteccao, data_hora, coordenadas, moto_id_moto, camera_id_camera, camera_id_patio, camera_id_filial) 
VALUES (6, TO_TIMESTAMP('2025-05-13 09:15:00', 'YYYY-MM-DD HH24:MI:SS'), 'Lat: -25.4284, Long: -49.2733', 6, 6, 6, 5);
INSERT INTO deteccao (id_deteccao, data_hora, coordenadas, moto_id_moto, camera_id_camera, camera_id_patio, camera_id_filial) 
VALUES (7, TO_TIMESTAMP('2025-05-13 09:30:00', 'YYYY-MM-DD HH24:MI:SS'), 'Lat: -12.9714, Long: -38.5014', 7, 7, 7, 6);
INSERT INTO deteccao (id_deteccao, data_hora, coordenadas, moto_id_moto, camera_id_camera, camera_id_patio, camera_id_filial) 
VALUES (8, TO_TIMESTAMP('2025-05-13 09:45:00', 'YYYY-MM-DD HH24:MI:SS'), 'Lat: -15.7942, Long: -47.8822', 8, 8, 8, 7);
INSERT INTO deteccao (id_deteccao, data_hora, coordenadas, moto_id_moto, camera_id_camera, camera_id_patio, camera_id_filial) 
VALUES (9, TO_TIMESTAMP('2025-05-13 10:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'Lat: -3.1190, Long: -60.0217', 9, 9, 9, 8);
INSERT INTO deteccao (id_deteccao, data_hora, coordenadas, moto_id_moto, camera_id_camera, camera_id_patio, camera_id_filial) 
VALUES (10, TO_TIMESTAMP('2025-05-13 10:15:00', 'YYYY-MM-DD HH24:MI:SS'), 'Lat: -8.0476, Long: -34.8770', 10, 10, 10, 9);

CREATE TABLE manutencao (
    id_manutencao NUMBER(9) NOT NULL,
    data          DATE NOT NULL,
    descricao     CLOB NOT NULL,
    moto_id_moto  NUMBER(9) NOT NULL,
    CONSTRAINT manutencao_pk PRIMARY KEY (id_manutencao, moto_id_moto),
    CONSTRAINT manutencao_moto_fk FOREIGN KEY (moto_id_moto)
        REFERENCES moto (id_moto)
);

INSERT INTO manutencao (id_manutencao, data, descricao, moto_id_moto) VALUES
(1, TO_DATE('2025-01-15', 'YYYY-MM-DD'), 'Troca de óleo e revisão básica.', 1);
INSERT INTO manutencao (id_manutencao, data, descricao, moto_id_moto) VALUES
(2, TO_DATE('2025-02-10', 'YYYY-MM-DD'), 'Substituição de pastilhas de freio.', 2);
INSERT INTO manutencao (id_manutencao, data, descricao, moto_id_moto) VALUES
(3, TO_DATE('2025-03-05', 'YYYY-MM-DD'), 'Reparo no sistema de ignição.', 3);
INSERT INTO manutencao (id_manutencao, data, descricao, moto_id_moto) VALUES
(4, TO_DATE('2025-03-20', 'YYYY-MM-DD'), 'Alinhamento e balanceamento.', 4);
INSERT INTO manutencao (id_manutencao, data, descricao, moto_id_moto) VALUES
(5, TO_DATE('2025-04-01', 'YYYY-MM-DD'), 'Troca da bateria.', 5);
INSERT INTO manutencao (id_manutencao, data, descricao, moto_id_moto) VALUES
(6, TO_DATE('2025-04-18', 'YYYY-MM-DD'), 'Limpeza e ajuste do carburador.', 6);
INSERT INTO manutencao (id_manutencao, data, descricao, moto_id_moto) VALUES
(7, TO_DATE('2025-04-25', 'YYYY-MM-DD'), 'Verificação do sistema elétrico.', 7);
INSERT INTO manutencao (id_manutencao, data, descricao, moto_id_moto) VALUES
(8, TO_DATE('2025-05-01', 'YYYY-MM-DD'), 'Substituição de corrente e coroa.', 8);
INSERT INTO manutencao (id_manutencao, data, descricao, moto_id_moto) VALUES
(9, TO_DATE('2025-05-05', 'YYYY-MM-DD'), 'Pintura e reparo de carenagem.', 9);
INSERT INTO manutencao (id_manutencao, data, descricao, moto_id_moto) VALUES
(10, TO_DATE('2025-05-10', 'YYYY-MM-DD'), 'Revisão geral preventiva.', 10);


CREATE TABLE usuario (
    id_usuario               NUMBER(9) NOT NULL,
    nome                     VARCHAR2(100) NOT NULL,
    cargo                    VARCHAR2(50) NOT NULL,
    email                    VARCHAR2(100) NOT NULL,
    manutencao_id_manutencao NUMBER(9) NOT NULL,
    manutencao_moto_id_moto  NUMBER(9) NOT NULL,
    CONSTRAINT id_usuario_pk PRIMARY KEY (id_usuario, manutencao_id_manutencao, manutencao_moto_id_moto),
    CONSTRAINT id_usuario_manutencao_fk FOREIGN KEY (manutencao_id_manutencao, manutencao_moto_id_moto)
        REFERENCES manutencao (id_manutencao, moto_id_moto)
);

INSERT INTO usuario (id_usuario, nome, cargo, email, manutencao_id_manutencao, manutencao_moto_id_moto) VALUES
(1, 'Carlos Silva', 'Mecânico', 'carlos.silva@example.com', 1, 1);
INSERT INTO usuario (id_usuario, nome, cargo, email, manutencao_id_manutencao, manutencao_moto_id_moto) VALUES
(2, 'Ana Souza', 'Técnica', 'ana.souza@example.com', 2, 2);
INSERT INTO usuario (id_usuario, nome, cargo, email, manutencao_id_manutencao, manutencao_moto_id_moto) VALUES
(3, 'João Pereira', 'Supervisor', 'joao.pereira@example.com', 3, 3);
INSERT INTO usuario (id_usuario, nome, cargo, email, manutencao_id_manutencao, manutencao_moto_id_moto) VALUES
(4, 'Mariana Lima', 'Analista', 'mariana.lima@example.com', 4, 4);
INSERT INTO usuario (id_usuario, nome, cargo, email, manutencao_id_manutencao, manutencao_moto_id_moto) VALUES
(5, 'Bruno Costa', 'Gestor de Frotas', 'bruno.costa@example.com', 5, 5);
INSERT INTO usuario (id_usuario, nome, cargo, email, manutencao_id_manutencao, manutencao_moto_id_moto) VALUES
(6, 'Fernanda Rocha', 'Coordenadora', 'fernanda.rocha@example.com', 6, 6);
INSERT INTO usuario (id_usuario, nome, cargo, email, manutencao_id_manutencao, manutencao_moto_id_moto) VALUES
(7, 'Lucas Almeida', 'Engenheiro', 'lucas.almeida@example.com', 7, 7);
INSERT INTO usuario (id_usuario, nome, cargo, email, manutencao_id_manutencao, manutencao_moto_id_moto) VALUES
(8, 'Patrícia Ramos', 'Mecânica', 'patricia.ramos@example.com', 8, 8);
INSERT INTO usuario (id_usuario, nome, cargo, email, manutencao_id_manutencao, manutencao_moto_id_moto) VALUES
(9, 'Thiago Martins', 'Técnico', 'thiago.martins@example.com', 9, 9);
INSERT INTO usuario (id_usuario, nome, cargo, email, manutencao_id_manutencao, manutencao_moto_id_moto) VALUES
(10, 'Juliana Ferreira', 'Assistente', 'juliana.ferreira@example.com', 10, 10);


CREATE TABLE relation_7 (
    sensor_iot_id_sensor NUMBER(9) NOT NULL,
    leitura_id_leitura   NUMBER(9) NOT NULL,
    CONSTRAINT relation_7_pk PRIMARY KEY (sensor_iot_id_sensor, leitura_id_leitura),
    CONSTRAINT relation_7_leitura_fk FOREIGN KEY (leitura_id_leitura)
        REFERENCES leitura (id_leitura),
    CONSTRAINT relation_7_sensor_iot_fk FOREIGN KEY (sensor_iot_id_sensor)
        REFERENCES sensor_iot (id_sensor)
);

INSERT INTO relation_7 (sensor_iot_id_sensor, leitura_id_leitura) VALUES (1, 1);
INSERT INTO relation_7 (sensor_iot_id_sensor, leitura_id_leitura) VALUES (2, 2);
INSERT INTO relation_7 (sensor_iot_id_sensor, leitura_id_leitura) VALUES (3, 3);
INSERT INTO relation_7 (sensor_iot_id_sensor, leitura_id_leitura) VALUES (4, 4);
INSERT INTO relation_7 (sensor_iot_id_sensor, leitura_id_leitura) VALUES (5, 5);
INSERT INTO relation_7 (sensor_iot_id_sensor, leitura_id_leitura) VALUES (6, 6);
INSERT INTO relation_7 (sensor_iot_id_sensor, leitura_id_leitura) VALUES (7, 7);
INSERT INTO relation_7 (sensor_iot_id_sensor, leitura_id_leitura) VALUES (8, 8);
INSERT INTO relation_7 (sensor_iot_id_sensor, leitura_id_leitura) VALUES (9, 9);
INSERT INTO relation_7 (sensor_iot_id_sensor, leitura_id_leitura) VALUES (10, 10);

//Consultas com JOIN, GROUP BY e ORDER BY

BEGIN
    -- 1. Total de manutenções por moto
    DBMS_OUTPUT.PUT_LINE('1. Total de manutenções por moto:');
    FOR r IN (
        SELECT m.moto_id_moto, COUNT(*) AS total_manutencoes
        FROM manutencao m
        GROUP BY m.moto_id_moto
        ORDER BY total_manutencoes DESC
    ) LOOP
        DBMS_OUTPUT.PUT_LINE('Moto ID: ' || r.moto_id_moto || ' - Manutenções: ' || r.total_manutencoes);
    END LOOP;

    -- 2. Total de leituras por sensor IoT
    DBMS_OUTPUT.PUT_LINE(CHR(10) || '2. Total de leituras por sensor IoT:');
    FOR r IN (
        SELECT s.id_sensor, COUNT(*) AS total_leituras
        FROM sensor_iot s
        JOIN relation_7 r7 ON s.id_sensor = r7.sensor_iot_id_sensor
        GROUP BY s.id_sensor
        ORDER BY total_leituras DESC
    ) LOOP
        DBMS_OUTPUT.PUT_LINE('Sensor ID: ' || r.id_sensor || ' - Leituras: ' || r.total_leituras);
    END LOOP;

    -- 3. Número de câmeras por filial
    DBMS_OUTPUT.PUT_LINE(CHR(10) || '3. Número de câmeras por filial:');
    FOR r IN (
        SELECT c.patio_id_filial AS filial_id, COUNT(*) AS total_cameras
        FROM camera c
        GROUP BY c.patio_id_filial
        ORDER BY total_cameras DESC
    ) LOOP
        DBMS_OUTPUT.PUT_LINE('Filial ID: ' || r.filial_id || ' - Câmeras: ' || r.total_cameras);
    END LOOP;
END;
/

BEGIN
    -- 1. Quantidade de detecções por câmera
    DBMS_OUTPUT.PUT_LINE('1. Detecções por câmera:');
    FOR r IN (
        SELECT d.camera_id_camera, COUNT(*) AS total_deteccoes
        FROM deteccao d
        GROUP BY d.camera_id_camera
        ORDER BY total_deteccoes DESC
    ) LOOP
        DBMS_OUTPUT.PUT_LINE('Câmera ID: ' || r.camera_id_camera || ' - Detecções: ' || r.total_deteccoes);
    END LOOP;

    -- 2. Total de manutenções por funcionário
    DBMS_OUTPUT.PUT_LINE(CHR(10) || '2. Total de manutenções por usuário:');
    FOR r IN (
        SELECT u.nome, COUNT(*) AS total
        FROM usuario u
        GROUP BY u.nome
        ORDER BY total DESC
    ) LOOP
        DBMS_OUTPUT.PUT_LINE('Usuário: ' || r.nome || ' - Manutenções: ' || r.total);
    END LOOP;

    -- 3. Detecções agrupadas por moto
    DBMS_OUTPUT.PUT_LINE(CHR(10) || '3. Detecções por moto:');
    FOR r IN (
        SELECT m.id_moto, COUNT(d.id_deteccao) AS total_deteccoes
        FROM moto m
        LEFT JOIN deteccao d ON m.id_moto = d.moto_id_moto
        GROUP BY m.id_moto
        ORDER BY total_deteccoes DESC
    ) LOOP
        DBMS_OUTPUT.PUT_LINE('Moto ID: ' || r.id_moto || ' - Detecções: ' || r.total_deteccoes);
    END LOOP;
END;
/

//Bloco PL/SQL para o Relatório.

DECLARE
    CURSOR c_motos IS
        SELECT
            id_moto,
            placa,
            LAG(placa, 1, 'Vazio') OVER (ORDER BY id_moto) AS placa_anterior,
            LEAD(placa, 1, 'Vazio') OVER (ORDER BY id_moto) AS placa_proxima
        FROM moto
        WHERE ROWNUM <= 5; -- Garante pelo menos 5 linhas
    
BEGIN
    DBMS_OUTPUT.PUT_LINE('ID_MOTO | PLACA ATUAL | PLACA ANTERIOR | PLACA PROXIMA');
    DBMS_OUTPUT.PUT_LINE('--------+--------------+----------------+----------------');
    
    FOR moto_rec IN c_motos LOOP
        DBMS_OUTPUT.PUT_LINE(
            LPAD(moto_rec.id_moto, 8) || ' | ' ||
            RPAD(moto_rec.placa, 12) || ' | ' ||
            RPAD(moto_rec.placa_anterior, 14) || ' | ' ||
            moto_rec.placa_proxima
        );
    END LOOP;
END;
/
