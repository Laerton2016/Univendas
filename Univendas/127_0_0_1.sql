
CREATE DATABASE IF NOT EXISTS `venda` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `venda`;

-- --------------------------------------------------------

--
-- Estrutura da tabela `itens_de_venda`
--

CREATE TABLE `itens_de_venda` (
   ID int(11) auto_increment,
  `ID_ITENS_DE_VENDA` int(11) NOT NULL,
  `COD_BARRAS` varchar(13) NOT NULL,
  `QUANTIDADE` int(11) NOT NULL,
  `VALOR_UNITARIO` double NOT NULL,
  `LOJA_ID_LOJA` varchar(1) NOT NULL,
  `VENDA_ID_VENDA` int(11) NOT NULL
  
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `itens_de_venda`
--

INSERT INTO `itens_de_venda` (`ID_ITENS_DE_VENDA`, `COD_BARRAS`, `QUANTIDADE`, `VALOR_UNITARIO`, `LOJA_ID_LOJA`, `VENDA_ID_VENDA`) VALUES
(122, '1000000006520', 2, 646.3823, 's', 59),
(123, '1000000005554', 1, 490.248, 's', 59);

-- --------------------------------------------------------

--
-- Estrutura da tabela `loja`
--

CREATE TABLE `loja` (
  `ID_LOJA` varchar(1) NOT NULL,
  `LOJA` varchar(255) NOT NULL,
  `ENDERECO_BANCO` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `loja`
--

INSERT INTO `loja` (`ID_LOJA`, `LOJA`, `ENDERECO_BANCO`) VALUES
('l', 'Laerton', 'asdfghjkl'),
('s', 'Sil', 'qwertyuiop');

-- --------------------------------------------------------

--
-- Estrutura da tabela `prevenda`
--

CREATE TABLE `prevenda` (
  `ID_PREVENDA` int(11) NOT NULL,
  `CONTROLE` int(11) NOT NULL,
  `VENDA_ID_VENDAS` int(11) NOT NULL,
  `LOJA_ID_LOJA` varchar(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `prevenda`
--

INSERT INTO `prevenda` (`ID_PREVENDA`, `CONTROLE`, `VENDA_ID_VENDAS`, `LOJA_ID_LOJA`) VALUES
(13, 3, 59, 's');

-- --------------------------------------------------------

--
-- Estrutura da tabela `usuario`
--

CREATE TABLE `usuario` (
  `ID_USUARIO` int(11) NOT NULL,
  `LOGIN` varchar(255) NOT NULL,
  `SENHA` varchar(255) NOT NULL,
  `TIPO` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `usuario`
--

INSERT INTO `usuario` (`ID_USUARIO`, `LOGIN`, `SENHA`, `TIPO`) VALUES
(1, 'daniel', '1234', 1),
(2, 'gibs', '3214', 2),
(3, 'mary', '1234', 3),
(4, 'adfds', 'asmdf3qr', 0),
(5, 'afghjhf', '12213', 0),
(6, 'afghjhf', '12213', 0);

-- --------------------------------------------------------

--
-- Estrutura da tabela `venda`
--

CREATE TABLE `venda` (
  `ID_VENDA` int(11) NOT NULL,
  `CLIENTE` int(11) NOT NULL DEFAULT '1',
  `VALOR_TOTAL` float NOT NULL,
  `MODALIDADE` varchar(255) NOT NULL,
  `OBS` longtext,
  `DATA` date NOT NULL,
  `USUARIO_ID_USUARIO` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `venda`
--

INSERT INTO `venda` (`ID_VENDA`, `CLIENTE`, `VALOR_TOTAL`, `MODALIDADE`, `OBS`, `DATA`, `USUARIO_ID_USUARIO`) VALUES
(59, 1, 1783.01, 'modalidade', '', '2017-01-13', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `itens_de_venda`
--
ALTER TABLE `itens_de_venda`
  ADD PRIMARY KEY (`ID_ITENS_DE_VENDA`),
  ADD KEY `fk_ITENS_DE_VENDA_LOJA1_idx` (`LOJA_ID_LOJA`),
  ADD KEY `fk_ITENS_DE_VENDA_VENDA1_idx` (`VENDA_ID_VENDA`);

--
-- Indexes for table `loja`
--
ALTER TABLE `loja`
  ADD PRIMARY KEY (`ID_LOJA`);

--
-- Indexes for table `prevenda`
--
ALTER TABLE `prevenda`
  ADD PRIMARY KEY (`ID_PREVENDA`,`VENDA_ID_VENDAS`,`LOJA_ID_LOJA`),
  ADD KEY `fk_PREVENDA_VENDA1_idx` (`VENDA_ID_VENDAS`),
  ADD KEY `fk_PREVENDA_LOJA1_idx` (`LOJA_ID_LOJA`);

--
-- Indexes for table `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`ID_USUARIO`);

--
-- Indexes for table `venda`
--
ALTER TABLE `venda`
  ADD PRIMARY KEY (`ID_VENDA`,`USUARIO_ID_USUARIO`),
  ADD KEY `fk_VENDA_USUARIO1_idx` (`USUARIO_ID_USUARIO`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `itens_de_venda`
--
ALTER TABLE `itens_de_venda`
  MODIFY `ID_ITENS_DE_VENDA` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=124;
--
-- AUTO_INCREMENT for table `prevenda`
--
ALTER TABLE `prevenda`
  MODIFY `ID_PREVENDA` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;
--
-- AUTO_INCREMENT for table `usuario`
--
ALTER TABLE `usuario`
  MODIFY `ID_USUARIO` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT for table `venda`
--
ALTER TABLE `venda`
  MODIFY `ID_VENDA` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=60;
--
-- Constraints for dumped tables
--

--
-- Limitadores para a tabela `itens_de_venda`
--
ALTER TABLE `itens_de_venda`
  ADD CONSTRAINT `fk_ITENS_DE_VENDA_LOJA1` FOREIGN KEY (`LOJA_ID_LOJA`) REFERENCES `loja` (`ID_LOJA`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_ITENS_DE_VENDA_VENDA1` FOREIGN KEY (`VENDA_ID_VENDA`) REFERENCES `venda` (`ID_VENDA`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `prevenda`
--
ALTER TABLE `prevenda`
  ADD CONSTRAINT `fk_PREVENDA_LOJA1` FOREIGN KEY (`LOJA_ID_LOJA`) REFERENCES `loja` (`ID_LOJA`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_PREVENDA_VENDA1` FOREIGN KEY (`VENDA_ID_VENDAS`) REFERENCES `venda` (`ID_VENDA`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Limitadores para a tabela `venda`
--
ALTER TABLE `venda`
  ADD CONSTRAINT `fk_VENDA_USUARIO1` FOREIGN KEY (`USUARIO_ID_USUARIO`) REFERENCES `usuario` (`ID_USUARIO`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
