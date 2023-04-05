# FIX Trading Simulator
![fix-trading-simulator](https://user-images.githubusercontent.com/29052879/229950673-bfda66ca-7d5c-4c0c-bdab-f31e1060afd1.gif)

## Sobre o projeto
Este projeto simula o processo de negociação financeira utilizando o protocolo FIX (Financial Information eXchange), um padrão de mensagens eletrônicas usado no mercado financeiro para comunicação entre instituições e traders.

1. **OrderGenerator**: Responsável pela geração de ordens de compra e venda fictícias utilizando o protocolo FIX.
2. **OrderAccumulator**: Encarregado de receber e processar as ordens geradas, simulando a acumulação de ordens no mercado financeiro.
Ao executar ambos os projetos simultaneamente, você verá a simulação de negociação em tempo real, mostrando como as ordens são geradas, transmitidas e processadas.

O projeto foi desenvolvido como uma oportunidade para aprender sobre o protocolo FIX e como ele funciona no mercado financeiro.

## Requisitos

- [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)
- IDE ou editor de código de sua preferência (ex: [Visual Studio](https://visualstudio.microsoft.com/), [Visual Studio Code](https://code.visualstudio.com/), [Rider](https://www.jetbrains.com/rider/))

## Como rodar o projeto

1. Clone o repositório:

```bash
git clone https://github.com/nicholasferrer/fix-trading-simulator.git
```
2. Navegue até a pasta do projeto:

```bash
cd fix-trading-simulator
```

3. Abra os projetos principais ```OrderGenerator.csproj``` e ```OrderAccumulator.csproj``` em sua IDE ou editor de código preferido.

4. Execute ambos os projetos simultaneamente.
