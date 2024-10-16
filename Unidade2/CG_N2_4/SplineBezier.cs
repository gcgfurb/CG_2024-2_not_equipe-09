using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System;

namespace gcgcg
{
  internal class SplineBezier : Objeto
  {
    private int qtdPontos;
    Ponto4D ponto1;
    Ponto4D ponto2;
    Ponto4D ponto3;
    Ponto4D ponto4;
    public SplineBezier(Objeto _paiRef, ref char _rotulo) : base(_paiRef, ref _rotulo)
    {
      PrimitivaTipo = PrimitiveType.LineStrip;

      ponto1 = new Ponto4D(-0.5, -0.5);
      ponto2 = new Ponto4D(-0.5, 0.5);
      ponto3 = new Ponto4D(0.5, 0.5);
      ponto4 = new Ponto4D(0.5, -0.5);

      base.PontosAdicionar(ponto1);
      base.PontosAdicionar(ponto2);
      base.PontosAdicionar(ponto3);
      base.PontosAdicionar(ponto4);
      
      this.qtdPontos = 10;

      Atualizar();
    }

    public void Atualizar()
    {
      pontosLista.Clear();

      for (double i = 0; i < this.qtdPontos; i++) {
        double t = i/(qtdPontos-1);
        Ponto4D p0 = FuncaoSpline(ponto1, ponto2, t);
        Ponto4D p1 = FuncaoSpline(ponto2, ponto3, t);
        Ponto4D p2 = FuncaoSpline(ponto3, ponto4, t);
        Ponto4D p01 = FuncaoSpline(p0, p1, t);
        Ponto4D p12 = FuncaoSpline(p1, p2, t);
        Ponto4D resultado = FuncaoSpline(p01, p12, t);

        base.PontosAdicionar(resultado);
      }
      
      base.ObjetoAtualizar();
    }

    public void MoverDireita(Objeto objetoSelecionado)
    {
      Ponto4D ponto = objetoSelecionado.PontosId(0);

      if (ponto1.X == ponto.X && ponto1.Y == ponto.Y) {
        ponto1 = new Ponto4D(ponto.X + 0.005, ponto.Y, 0);
      } else if (ponto2.X == ponto.X && ponto2.Y == ponto.Y) {
        ponto2 = new Ponto4D(ponto.X + 0.005, ponto.Y, 0);
      } else if (ponto3.X == ponto.X && ponto3.Y == ponto.Y) {
        ponto3 = new Ponto4D(ponto.X + 0.005, ponto.Y, 0);
      } else if (ponto4.X == ponto.X && ponto4.Y == ponto.Y) {
        ponto4 = new Ponto4D(ponto.X + 0.005, ponto.Y, 0);
      }
      Atualizar();
    }

    public void MoverEsquerda(Objeto objetoSelecionado)
    {
      Ponto4D ponto = objetoSelecionado.PontosId(0);

      if (ponto1.X == ponto.X && ponto1.Y == ponto.Y) {
        ponto1 = new Ponto4D(ponto.X - 0.005, ponto.Y, 0);
      } else if (ponto2.X == ponto.X && ponto2.Y == ponto.Y) {
        ponto2 = new Ponto4D(ponto.X - 0.005, ponto.Y, 0);
      } else if (ponto3.X == ponto.X && ponto3.Y == ponto.Y) {
        ponto3 = new Ponto4D(ponto.X - 0.005, ponto.Y, 0);
      } else if (ponto4.X == ponto.X && ponto4.Y == ponto.Y) {
        ponto4 = new Ponto4D(ponto.X - 0.005, ponto.Y, 0);
      }
      Atualizar();
    }

    public void MoverCima(Objeto objetoSelecionado)
    {
      Ponto4D ponto = objetoSelecionado.PontosId(0);

      if (ponto1.X == ponto.X && ponto1.Y == ponto.Y) {
        ponto1 = new Ponto4D(ponto.X, ponto.Y + 0.005, 0);
      } else if (ponto2.X == ponto.X && ponto2.Y == ponto.Y) {
        ponto2 = new Ponto4D(ponto.X, ponto.Y + 0.005, 0);
      } else if (ponto3.X == ponto.X && ponto3.Y == ponto.Y) {
        ponto3 = new Ponto4D(ponto.X, ponto.Y + 0.005, 0);
      } else if (ponto4.X == ponto.X && ponto4.Y == ponto.Y) {
        ponto4 = new Ponto4D(ponto.X, ponto.Y + 0.005, 0);
      }
      Atualizar();
    }

    public void MoverBaixo(Objeto objetoSelecionado)
    {
      Ponto4D ponto = objetoSelecionado.PontosId(0);

      if (ponto1.X == ponto.X && ponto1.Y == ponto.Y) {
        ponto1 = new Ponto4D(ponto.X, ponto.Y - 0.005, 0);
      } else if (ponto2.X == ponto.X && ponto2.Y == ponto.Y) {
        ponto2 = new Ponto4D(ponto.X, ponto.Y - 0.005, 0);
      } else if (ponto3.X == ponto.X && ponto3.Y == ponto.Y) {
        ponto3 = new Ponto4D(ponto.X, ponto.Y - 0.005, 0);
      } else if (ponto4.X == ponto.X && ponto4.Y == ponto.Y) {
        ponto4 = new Ponto4D(ponto.X, ponto.Y - 0.005, 0);
      }
      Atualizar();
    }
    
    private Ponto4D FuncaoSpline(Ponto4D ponto1, Ponto4D ponto2, double t)
    {
      double pontoX = ponto1.X + (ponto2.X - ponto1.X) * t;
      double pontoY = ponto1.Y + (ponto2.Y - ponto1.Y) * t;

      return new Ponto4D(pontoX, pontoY);
    }
    
    public void AlterarQuantidadePontos(int incremento)
    {
      qtdPontos += incremento;
      if (qtdPontos < 2) qtdPontos = 2; // Evitar quantidade negativa ou zero
      Atualizar();
    }
  }
}
