-- =============================================================================
-- 2TDSPJ_2025_Drops_Integrantes.sql
-- Script para limpeza completa do banco de dados
-- Integrantes: Gabriel Camargo (RM557879), Kauan Felipe (RM557954), Vinicius Alves (RM551939)
-- =============================================================================

-- IMPORTANTE: Execute este script SOMENTE se desejar remover TODOS os objetos do banco!
-- Este script irá apagar permanentemente:
-- - Todas as tabelas e seus dados
-- - Todas as funções personalizadas
-- - Todos os procedimentos armazenados
-- - Todos os triggers
-- - Todas as sequences (se houver)

-- =============================================================================
-- CONFIRMAÇÃO DE EXECUÇÃO
-- =============================================================================

SET SERVEROUTPUT ON
BEGIN
    DBMS_OUTPUT.PUT_LINE('=============================================================');
    DBMS_OUTPUT.PUT_LINE('ATENÇÃO: SCRIPT DE LIMPEZA COMPLETA DO BANCO DE DADOS');
    DBMS_OUTPUT.PUT_LINE('=============================================================');
    DBMS_OUTPUT.PUT_LINE('Este script irá REMOVER PERMANENTEMENTE:');
    DBMS_OUTPUT.PUT_LINE('- Todas as tabelas da Sprint 3 (Mottu)');
    DBMS_OUTPUT.PUT_LINE('- Funções: converter_para_json, validar_placa_moto');
    DBMS_OUTPUT.PUT_LINE('- Procedimentos: exibir_motos_json, relatorio_custos_manutencao');
    DBMS_OUTPUT.PUT_LINE('- Trigger: tr_auditoria_moto');
    DBMS_OUTPUT.PUT_LINE('- Tabela de auditoria e todos os dados');
    DBMS_OUTPUT.PUT_LINE('=============================================================');
    DBMS_OUTPUT.PUT_LINE('Início da execução: ' || TO_CHAR(SYSDATE, 'DD/MM/YYYY HH24:MI:SS'));
    DBMS_OUTPUT.PUT_LINE('=============================================================');
END;
/

-- =============================================================================
-- 1. REMOÇÃO DE TRIGGERS
-- =============================================================================

PROMPT Removendo triggers...

BEGIN
    EXECUTE IMMEDIATE 'DROP TRIGGER tr_auditoria_moto';
    DBMS_OUTPUT.PUT_LINE('✓ Trigger tr_auditoria_moto removido');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Trigger tr_auditoria_moto não encontrado');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover trigger: ' || SQLERRM);
        END IF;
END;
/

-- =============================================================================
-- 2. REMOÇÃO DE PROCEDIMENTOS
-- =============================================================================

PROMPT Removendo procedimentos...

BEGIN
    EXECUTE IMMEDIATE 'DROP PROCEDURE exibir_motos_json';
    DBMS_OUTPUT.PUT_LINE('✓ Procedimento exibir_motos_json removido');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Procedimento exibir_motos_json não encontrado');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover procedimento: ' || SQLERRM);
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP PROCEDURE relatorio_custos_manutencao';
    DBMS_OUTPUT.PUT_LINE('✓ Procedimento relatorio_custos_manutencao removido');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Procedimento relatorio_custos_manutencao não encontrado');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover procedimento: ' || SQLERRM);
        END IF;
END;
/

-- =============================================================================
-- 3. REMOÇÃO DE FUNÇÕES
-- =============================================================================

PROMPT Removendo funções...

BEGIN
    EXECUTE IMMEDIATE 'DROP FUNCTION converter_para_json';
    DBMS_OUTPUT.PUT_LINE('✓ Função converter_para_json removida');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Função converter_para_json não encontrada');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover função: ' || SQLERRM);
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP FUNCTION validar_placa_moto';
    DBMS_OUTPUT.PUT_LINE('✓ Função validar_placa_moto removida');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Função validar_placa_moto não encontrada');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover função: ' || SQLERRM);
        END IF;
END;
/

-- =============================================================================
-- 4. REMOÇÃO DE TABELAS (ORDEM RESPEITANDO FOREIGN KEYS)
-- =============================================================================

PROMPT Removendo tabelas em ordem de dependência...

-- 4.1 Tabelas dependentes (com foreign keys)
BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE usuario CASCADE CONSTRAINTS';
    DBMS_OUTPUT.PUT_LINE('✓ Tabela usuario removida');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Tabela usuario não encontrada');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover tabela usuario: ' || SQLERRM);
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE manutencao CASCADE CONSTRAINTS';
    DBMS_OUTPUT.PUT_LINE('✓ Tabela manutencao removida');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Tabela manutencao não encontrada');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover tabela manutencao: ' || SQLERRM);
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE deteccao CASCADE CONSTRAINTS';
    DBMS_OUTPUT.PUT_LINE('✓ Tabela deteccao removida');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Tabela deteccao não encontrada');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover tabela deteccao: ' || SQLERRM);
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE sensor_leitura CASCADE CONSTRAINTS';
    DBMS_OUTPUT.PUT_LINE('✓ Tabela sensor_leitura removida');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Tabela sensor_leitura não encontrada');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover tabela sensor_leitura: ' || SQLERRM);
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE sensor_iot CASCADE CONSTRAINTS';
    DBMS_OUTPUT.PUT_LINE('✓ Tabela sensor_iot removida');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Tabela sensor_iot não encontrada');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover tabela sensor_iot: ' || SQLERRM);
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE moto CASCADE CONSTRAINTS';
    DBMS_OUTPUT.PUT_LINE('✓ Tabela moto removida');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Tabela moto não encontrada');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover tabela moto: ' || SQLERRM);
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE leitura CASCADE CONSTRAINTS';
    DBMS_OUTPUT.PUT_LINE('✓ Tabela leitura removida');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Tabela leitura não encontrada');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover tabela leitura: ' || SQLERRM);
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE camera CASCADE CONSTRAINTS';
    DBMS_OUTPUT.PUT_LINE('✓ Tabela camera removida');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Tabela camera não encontrada');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover tabela camera: ' || SQLERRM);
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE patio CASCADE CONSTRAINTS';
    DBMS_OUTPUT.PUT_LINE('✓ Tabela patio removida');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Tabela patio não encontrada');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover tabela patio: ' || SQLERRM);
        END IF;
END;
/

-- 4.2 Tabelas principais (sem dependências)
BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE statusmoto CASCADE CONSTRAINTS';
    DBMS_OUTPUT.PUT_LINE('✓ Tabela statusmoto removida');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Tabela statusmoto não encontrada');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover tabela statusmoto: ' || SQLERRM);
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE filial CASCADE CONSTRAINTS';
    DBMS_OUTPUT.PUT_LINE('✓ Tabela filial removida');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Tabela filial não encontrada');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover tabela filial: ' || SQLERRM);
        END IF;
END;
/

-- 4.3 Tabela de auditoria
BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE auditoria CASCADE CONSTRAINTS';
    DBMS_OUTPUT.PUT_LINE('✓ Tabela auditoria removida');
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE = -942 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ Tabela auditoria não encontrada');
        ELSE
            DBMS_OUTPUT.PUT_LINE('✗ Erro ao remover tabela auditoria: ' || SQLERRM);
        END IF;
END;
/

-- =============================================================================
-- 5. REMOÇÃO DE SEQUENCES (SE EXISTIREM)
-- =============================================================================

PROMPT Verificando e removendo sequences...

BEGIN
    FOR seq IN (SELECT sequence_name FROM user_sequences WHERE sequence_name LIKE '%MOTTU%' OR sequence_name LIKE '%AUDITORIA%') 
    LOOP
        EXECUTE IMMEDIATE 'DROP SEQUENCE ' || seq.sequence_name;
        DBMS_OUTPUT.PUT_LINE('✓ Sequence ' || seq.sequence_name || ' removida');
    END LOOP;
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('⚠ Nenhuma sequence relacionada encontrada ou erro: ' || SQLERRM);
END;
/

-- =============================================================================
-- 6. LIMPEZA DE LIXEIRA (OPCIONAL)
-- =============================================================================

PROMPT Limpando lixeira do Oracle...

BEGIN
    FOR obj IN (SELECT object_name, type FROM user_recyclebin WHERE original_name LIKE '%MOTTU%' 
                OR original_name LIKE 'FILIAL%' 
                OR original_name LIKE 'MOTO%'
                OR original_name LIKE 'PATIO%'
                OR original_name LIKE 'AUDITORIA%') 
    LOOP
        EXECUTE IMMEDIATE 'PURGE ' || obj.type || ' "' || obj.object_name || '"';
        DBMS_OUTPUT.PUT_LINE('✓ Objeto ' || obj.object_name || ' removido da lixeira');
    END LOOP;
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('⚠ Erro na limpeza da lixeira ou nenhum objeto encontrado: ' || SQLERRM);
END;
/

-- =============================================================================
-- 7. VERIFICAÇÃO FINAL
-- =============================================================================

PROMPT Realizando verificação final...

-- Verificar se ainda existem objetos relacionados
BEGIN
    DECLARE
        v_count NUMBER;
        v_objetos_restantes BOOLEAN := FALSE;
    BEGIN
        -- Verificar tabelas
        SELECT COUNT(*) INTO v_count 
        FROM user_tables 
        WHERE table_name IN ('FILIAL','STATUSMOTO','PATIO','CAMERA','LEITURA','MOTO','SENSOR_IOT',
                             'SENSOR_LEITURA','DETECCAO','MANUTENCAO','USUARIO','AUDITORIA');
        
        IF v_count > 0 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ ATENÇÃO: ' || v_count || ' tabela(s) ainda existem');
            v_objetos_restantes := TRUE;
        END IF;
        
        -- Verificar funções
        SELECT COUNT(*) INTO v_count 
        FROM user_objects 
        WHERE object_type = 'FUNCTION' AND object_name IN ('CONVERTER_PARA_JSON','VALIDAR_PLACA_MOTO');
        
        IF v_count > 0 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ ATENÇÃO: ' || v_count || ' função(ões) ainda existem');
            v_objetos_restantes := TRUE;
        END IF;
        
        -- Verificar procedimentos
        SELECT COUNT(*) INTO v_count 
        FROM user_objects 
        WHERE object_type = 'PROCEDURE' AND object_name IN ('EXIBIR_MOTOS_JSON','RELATORIO_CUSTOS_MANUTENCAO');
        
        IF v_count > 0 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ ATENÇÃO: ' || v_count || ' procedimento(s) ainda existem');
            v_objetos_restantes := TRUE;
        END IF;
        
        -- Verificar triggers
        SELECT COUNT(*) INTO v_count 
        FROM user_triggers 
        WHERE trigger_name = 'TR_AUDITORIA_MOTO';
        
        IF v_count > 0 THEN
            DBMS_OUTPUT.PUT_LINE('⚠ ATENÇÃO: ' || v_count || ' trigger(s) ainda existem');
            v_objetos_restantes := TRUE;
        END IF;
        
        -- Resultado final
        IF NOT v_objetos_restantes THEN
            DBMS_OUTPUT.PUT_LINE('');
            DBMS_OUTPUT.PUT_LINE('=============================================================');
            DBMS_OUTPUT.PUT_LINE('✅ LIMPEZA CONCLUÍDA COM SUCESSO!');
            DBMS_OUTPUT.PUT_LINE('Todos os objetos da Sprint 3 foram removidos do banco de dados.');
            DBMS_OUTPUT.PUT_LINE('=============================================================');
        ELSE
            DBMS_OUTPUT.PUT_LINE('');
            DBMS_OUTPUT.PUT_LINE('=============================================================');
            DBMS_OUTPUT.PUT_LINE('⚠ LIMPEZA PARCIAL');
            DBMS_OUTPUT.PUT_LINE('Alguns objetos podem ainda existir. Verifique manualmente.');
            DBMS_OUTPUT.PUT_LINE('=============================================================');
        END IF;
        
        DBMS_OUTPUT.PUT_LINE('Fim da execução: ' || TO_CHAR(SYSDATE, 'DD/MM/YYYY HH24:MI:SS'));
    END;
END;
/

-- =============================================================================
-- 8. COMANDOS MANUAIS DE EMERGÊNCIA (COMENTADOS)
-- =============================================================================

/*
-- Se o script automatizado falhar, execute os comandos abaixo manualmente:
-- ATENÇÃO: Descomente apenas se necessário!

-- DROPS MANUAIS DE EMERGÊNCIA:

DROP TRIGGER tr_auditoria_moto;
DROP PROCEDURE exibir_motos_json;
DROP PROCEDURE relatorio_custos_manutencao;
DROP FUNCTION converter_para_json;
DROP FUNCTION validar_placa_moto;

DROP TABLE usuario CASCADE CONSTRAINTS;
DROP TABLE manutencao CASCADE CONSTRAINTS;
DROP TABLE deteccao CASCADE CONSTRAINTS;
DROP TABLE sensor_leitura CASCADE CONSTRAINTS;
DROP TABLE sensor_iot CASCADE CONSTRAINTS;
DROP TABLE moto CASCADE CONSTRAINTS;
DROP TABLE leitura CASCADE CONSTRAINTS;
DROP TABLE camera CASCADE CONSTRAINTS;
DROP TABLE patio CASCADE CONSTRAINTS;
DROP TABLE statusmoto CASCADE CONSTRAINTS;
DROP TABLE filial CASCADE CONSTRAINTS;
DROP TABLE auditoria CASCADE CONSTRAINTS;

-- Limpeza completa da lixeira:
PURGE RECYCLEBIN;
*/

-- =============================================================================
-- FIM DO SCRIPT DE DROPS
-- =============================================================================

PROMPT
PROMPT Script de limpeza finalizado.
PROMPT Para recriar os objetos, execute o arquivo: 2TDSPJ_2025_CodigoSql_Integrantes.sql
PROMPT