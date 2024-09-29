using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System;

namespace gcgcg
{
  internal class SplineBezier : Objeto
  {
    public SplineBezier(Objeto _paiRef, ref char _rotulo) : base(_paiRef, ref _rotulo)
    {
      PontosListaTamanho = 11; // Quantidade inicial de pontos na spline
      PrimitivaTipo = PrimitiveType.LineStrip; // Desenho da spline
      Atualizar();
    }

    public void SplineQtdPto(int inc)
    {
      PontosListaTamanho += inc;
      if (PontosListaTamanho < 2) PontosListaTamanho = 2; // Evitar quantidade negativa ou zero
      Atualizar();
    }

    public void Atualizar()
    {
      pontosLista.Clear();
      for (int i = 0; i < PontosListaTamanho; i++)
      {
        // Algoritmo para calcular a spline
        double t = i / (double)(PontosListaTamanho - 1);
        Ponto4D pontoSpline = CalcularSpline(t);
        PontosAdicionar(pontoSpline);
      }
      base.ObjetoAtualizar();
    }

    public void AtualizarSpline(Ponto4D ptoInc, bool proximo)
    {
      pontosLista[proximo] = ptoInc;
      Atualizar();
    }

    private Ponto4D CalcularSpline(double t)
    {
      int n = pontosLista.Count - 1; // Grau da curva
      Ponto4D pontoBezier = new Ponto4D(0, 0, 0);

      for (int i = 0; i <= n; i++)
      {
          double coefBinomial = Binomial(n, i);
          double bernstein = coefBinomial * Math.Pow(1 - t, n - i) * Math.Pow(t, i);
          pontoBezier.X += bernstein * pontosLista[i].X;
          pontoBezier.Y += bernstein * pontosLista[i].Y * -1;
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
  }
}
