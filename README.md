# Calculadora de Juros Compostos

Aplicativo desktop em Windows Forms que projeta a evolução de um investimento com aportes
mensais sob juros compostos, mês a mês.

Você informa o valor inicial, o aporte mensal, a taxa de juros anual e o prazo. O resultado
é uma tabela com uma linha por mês, mostrando os juros creditados, o total investido, o
total de juros acumulados e o montante final.

## Como usar

Os campos de valor e de taxa têm máscara dinâmica: os dígitos entram pela direita, no
estilo caixa eletrônico. Digitar `4`, `0`, `0`, `0`, `0` produz `R$ 400,00`, e o backspace
apaga um dígito por vez.

O prazo pode ser informado em **anos** ou em **meses**, escolhido no combo ao lado. A tecla
Enter avança para o campo seguinte.

## O cálculo

**Taxa.** A taxa é informada ao ano e convertida na mensal equivalente por capitalização
composta:

```
taxaMensal = ((1 + taxaAnual) ^ (1/12)) - 1
```

Dividir a taxa anual por 12 daria a taxa linear, que ao ser composta por doze meses supera
a taxa contratada — 12% ao ano viraria 12,68% efetivos. A equivalência composta devolve
exatamente os 12%.

**Aportes.** O aporte é postecipado: entra ao fim do mês e passa a render a partir do mês
seguinte. Os juros de cada mês incidem sobre o saldo anterior ao aporte daquele mês.

**Valores monetários.** Todo cálculo usa `decimal`, nunca `double`. Os juros de cada mês são
arredondados a centavos pela convenção comercial, em que o meio vai para cima em valor
absoluto — `0,125` vira `0,13`, e não `0,12` como faria o arredondamento bancário, que é o
padrão do .NET.

## Arquitetura

Três camadas, com as dependências apontando sempre para dentro:

```
UI  ──▶  Aplicacao  ──▶  Dominio
```

O **Domínio** não referencia nenhum outro projeto. É onde vive a regra financeira, livre de
qualquer conhecimento de tela, de formatação ou de plataforma.

A **Aplicação** expõe o caso de uso `ISimularInvestimento`. Ela traduz valores crus nos tipos
do domínio, aplica as regras de aceitação da entrada e devolve DTOs. É a fronteira que
permite ao domínio mudar sem quebrar a apresentação.

A **UI** lê os campos, monta a requisição, exibe o resultado e cuida da máscara e da
formatação em pt-BR. Não contém cálculo financeiro.

### Estrutura

```
CalculadoraJurosCompostos.slnx
├── src/
│   ├── CalculadoraJurosCompostos.Dominio      net10.0
│   │     TaxaDeJuros, PeriodoDeInvestimento, ParametrosSimulacao,
│   │     EvolucaoMensal, ResultadoSimulacao, SimuladorDeJurosCompostos, Moeda
│   ├── CalculadoraJurosCompostos.Aplicacao    net10.0
│   │     ISimularInvestimento, SimulacaoRequest, SimulacaoResponse,
│   │     TipoPeriodo, SimulacaoInvalidaException, ServicosDaAplicacao
│   └── CalculadoraJurosCompostos.UI           net10.0-windows
│         FrmCalculadoraJurosCompostos, Program, Mascaras/
└── tests/
    ├── CalculadoraJurosCompostos.Testes       net10.0
    └── CalculadoraJurosCompostos.UI.Testes    net10.0-windows
```

As dependências são resolvidas por injeção, com o composition root em `Program.cs`. O
registro dos serviços fica em `ServicosDaAplicacao`, dentro da própria camada de aplicação,
para que a apresentação não precise conhecer o domínio.

## Requisitos

- SDK do .NET 10
- Windows (a apresentação é Windows Forms)

## Build, testes e execução

```bash
dotnet build
```

```bash
dotnet test
```

```bash
dotnet run --project src/CalculadoraJurosCompostos.UI
```

Os testes de regra de negócio ficam em `CalculadoraJurosCompostos.Testes`, que tem target
`net10.0` sem sufixo de plataforma — regra financeira não depende de Windows. Os testes da
apresentação ficam em `CalculadoraJurosCompostos.UI.Testes`.

Entre o que a suíte cobre: a equivalência da taxa verificada pela propriedade que a define
(compor a mensal por doze meses tem que devolver a anual), o aporte postecipado, o
mapeamento de ano e mês nos pontos de virada, as validações de entrada, o arredondamento
monetário e a máscara dos campos, incluindo idempotência da reaplicação.

## Observação sobre o Visual Studio

Alterar o `TargetFramework` de um projeto já carregado, ou mover pastas de projeto, com a
solution aberta leva o Visual Studio a trabalhar com uma avaliação em cache e sobrescrever
o `obj/project.assets.json`. Feche a solution antes dessas operações.
