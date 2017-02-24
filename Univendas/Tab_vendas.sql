drop database if exists `venda`;
CREATE DATABASE IF NOT EXISTS `venda` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `venda`;

-- CRIA A TABELA DE USUÁRIO

CREATE TABLE `usuario` (
  `ID_USUARIO` int(11) auto_increment,
  `LOGIN` varchar(255) NOT NULL,
  `SENHA` varchar(255) NOT NULL,
  `TIPO` int(11) NOT NULL,
  primary key (`ID_USUARIO`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Estrutura da tabela `venda`
--

CREATE TABLE `venda` (
  `ID_VENDA` int(11) auto_increment,
  `CLIENTE` int(11) NOT NULL DEFAULT '1',
  `VALOR_TOTAL` float NOT NULL,
  `MODALIDADE` varchar(255) NOT NULL,
  `OBS` longtext,
  `DATA` date NOT NULL,
  `USUARIO_ID_USUARIO` int(11) NOT NULL,
  `CANCELADO` tinyint(1) not null default 0,
  primary KEY (`ID_VENDA`),
  CONSTRAINT `fk_VENDA_USUARIO1` FOREIGN KEY (`USUARIO_ID_USUARIO`) REFERENCES `usuario` (`ID_USUARIO`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Estrutura da tabela `loja`
--

CREATE TABLE `loja` (
  `ID_LOJA` varchar(1) NOT NULL,
  `LOJA` varchar(255) NOT NULL,
  `ENDERECO_BANCO` longtext NOT NULL,
  primary KEY (`ID_LOJA`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Estrutura da tabela `itens_de_venda`
--

CREATE TABLE `itens_de_venda` (
   `ID` int(11) auto_increment,
  `ID_ITENS_DE_VENDA` int(11) NOT NULL,
  `COD_BARRAS` varchar(13) NOT NULL,
  `QUANTIDADE` int(11) NOT NULL,
  `VALOR_UNITARIO` double NOT NULL,
  `LOJA_ID_LOJA` varchar(1) NOT NULL,
  `VENDA_ID_VENDA` int(11) NOT NULL,
  PRIMARY key (`ID`),
  constraint `fk_ITENS_DE_VENDA_LOJA1_idx` foreign key (`LOJA_ID_LOJA`) references `LOJA`(`ID_LOJA`),
  constraint `fk_ITENS_DE_VENDA_VENDA1_idx`foreign key  (`VENDA_ID_VENDA`) references `VENDA`(`ID_VENDA`)
  
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Estrutura da tabela `prevenda`
--

CREATE TABLE `prevenda` (
  `ID_PREVENDA` int(11) auto_increment,
  `CONTROLE` int(11) NOT NULL,
  `VENDA_ID_VENDAS` int(11) NOT NULL,
  `LOJA_ID_LOJA` varchar(1) NOT NULL,
  `REALIZADO` tinyint(1) default 0,
  primary KEY (`ID_PREVENDA`),
  constraint `fk_PREVENDA_VENDA1_idx` foreign key (`VENDA_ID_VENDAS`) references `VENDA`(`ID_VENDA`),
  constraint `fk_PREVENDA_LOJA1_idx` foreign key (`LOJA_ID_LOJA`) references `LOJA` (`ID_LOJA`)

) ENGINE=InnoDB DEFAULT CHARSET=utf8;



INSERT INTO `loja` (`ID_LOJA`, `LOJA`, `ENDERECO_BANCO`) VALUES
('l', 'Laerton', 'asdfghjkl'),
('s', 'Sil', 'qwertyuiop');

INSERT INTO `usuario` (`ID_USUARIO`, `LOGIN`, `SENHA`, `TIPO`) VALUES
(1, 'daniel', '1234', 1),
(2, 'gibs', '3214', 2),
(3, 'mary', '1234', 3),
(4, 'adfds', 'asmdf3qr', 0),
(5, 'afghjhf', '12213', 0),
(6, 'afghjhf', '12213', 0);

INSERT INTO `venda` (`ID_VENDA`, `CLIENTE`, `VALOR_TOTAL`, `MODALIDADE`, `OBS`, `DATA`, `USUARIO_ID_USUARIO`) VALUES
(59, 1, 1783.01, 'modalidade', '', '2017-01-13', 1);


INSERT INTO `itens_de_venda` (`ID_ITENS_DE_VENDA`, `COD_BARRAS`, `QUANTIDADE`, `VALOR_UNITARIO`, `LOJA_ID_LOJA`, `VENDA_ID_VENDA`) VALUES
(122, '1000000006520', 2, 646.3823, 's', 59),
(123, '1000000005554', 1, 490.248, 's', 59);
