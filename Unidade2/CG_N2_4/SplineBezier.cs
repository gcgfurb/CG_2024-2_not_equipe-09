using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System;

namespace gcgcg
{
  internal class SplineBezier : Objeto
  {
    private int qtdPontos;
    private List<Ponto4D> pontosControle;

    public SplineBezier(Objeto _paiRef, ref char _rotulo, List<Ponto4D> pontosControle) : base(_paiRef, ref _rotulo)
    {
      this.pontosControle = pontosControle;
      qtdPontos = 11; // Quantidade inicial de pontos na spline
      PrimitivaTipo = PrimitiveType.LineStrip; // Desenho da spline
      Atualizar();
    }

    public void Atualizar()
    {
      pontosLista.Clear();
      for (int i = 0; i < qtdPontos; i++)
      {
        // Algoritmo para calcular a spline
        double t = i / (double)(qtdPontos - 1);
        Ponto4D pontoSpline = CalcularSpline(t);
        PontosAdicionar(pontoSpline);
      }
      base.ObjetoAtualizar();
    }

    private Ponto4D CalcularSpline(double t)
    {
      int n = pontosControle.Count - 1; // Grau da curva
      Ponto4D pontoBezier = new Ponto4D(0, 0, 0);

      for (int i = 0; i <= n; i++)
      {
          double coefBinomial = Binomial(n, i);
          double bernstein = coefBinomial * Math.Pow(1 - t, n - i) * Math.Pow(t, i);
          pontoBezier.X += bernstein * pontosControle[i].X;
          pontoBezier.Y += bernstein * pontosControle[i].Y * -1;
      }

      return pontoBezier;
    }

    // Função para calcular o coeficiente binomial (n sobre i)
    private double Binomial(int n, int i)
    {
      return Fatorial(n) / (Fatorial(i) * Fatorial(n - i));
    }

    // Função para calcular o fatorial de um número
    private double Fatorial(int num)
    {
      if (num == 0 || num == 1)
        return 1;
      return num * Fatorial(num - 1);
    }

    public void AlterarQuantidadePontos(int incremento)
    {
      qtdPontos += incremento;
      if (qtdPontos < 2) qtdPontos = 2; // Evitar quantidade negativa ou zero
      Atualizar();
    }

    public void AtualizarPontoControle(int indice, Ponto4D novoPonto)
    {
      pontosControle[indice] = novoPonto;
      Atualizar(); // Recalcular a spline quando um ponto de controle mudar
    }
  }
}
