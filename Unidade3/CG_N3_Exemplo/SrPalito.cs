using OpenTK.Mathematics;
using CG_Biblioteca;

namespace gcgcg
{
    internal class SrPalito : Objeto
    {
        private SegReta segmento;
        private double raio;
        private double angulo;
        private Ponto4D pontoOrigem;
        private Ponto4D pontoFinal;

        public SrPalito(Objeto _paiRef, ref char rotulo) : base(_paiRef, ref rotulo)
        {
            raio = 0.5;
            angulo = 45;
            segmento = new SegReta(_paiRef, ref rotulo, new Ponto4D(0, 0), Matematica.GerarPtosCirculo(angulo, raio));
            pontoOrigem = new Ponto4D(segmento.PontosId(0).X, segmento.PontosId(0).Y);
            pontoFinal = new Ponto4D(segmento.PontosId(segmento.PontosListaTamanho-1).X, segmento.PontosId(segmento.PontosListaTamanho-1).Y);
        }

        public void MoverEsquerda()
        {
            pontoOrigem = new Ponto4D(segmento.PontosId(0).X - 0.0001, segmento.PontosId(0).Y);
            pontoFinal = new Ponto4D(segmento.PontosId(segmento.PontosListaTamanho-1).X - 0.0001, segmento.PontosId(segmento.PontosListaTamanho-1).Y);
            segmento.PontosAlterar(pontoOrigem, 0);
            segmento.PontosAlterar(pontoFinal, segmento.PontosListaTamanho-1);
            segmento.ObjetoAtualizar();
        }

        public void MoverDireita()
        {
            pontoOrigem = new Ponto4D(segmento.PontosId(0).X + 0.0001, segmento.PontosId(0).Y);
            pontoFinal = new Ponto4D(segmento.PontosId(segmento.PontosListaTamanho-1).X + 0.0001, segmento.PontosId(segmento.PontosListaTamanho-1).Y);
            segmento.PontosAlterar(pontoOrigem, 0);
            segmento.PontosAlterar(pontoFinal, segmento.PontosListaTamanho-1);
            segmento.ObjetoAtualizar();
        }

        public void DiminuirRaio()
        {
            pontoFinal = new Ponto4D(pontoFinal.X - 0.0001, pontoFinal.Y - 0.0001);
            segmento.PontosAlterar(pontoFinal, segmento.PontosListaTamanho-1);
            raio -= 0.0001;
            segmento.ObjetoAtualizar();
        }

        public void AumentarRaio()
        {
            pontoFinal = new Ponto4D(pontoFinal.X + 0.0001, pontoFinal.Y + 0.0001);
            segmento.PontosAlterar(pontoFinal, segmento.PontosListaTamanho-1);
            raio += 0.0001;
            segmento.ObjetoAtualizar();
        }

        public void DiminuirAngulo()
        {
            angulo -= 0.01; // Reduz o ângulo em 5 graus
            pontoFinal = Matematica.GerarPtosCirculo(angulo, raio);
            AtualizarSegmento();
        }

        public void AumentarAngulo()
        {
            angulo += 0.01; // Aumenta o ângulo em 5 graus
            pontoFinal = Matematica.GerarPtosCirculo(angulo, raio);
            AtualizarSegmento();
        }

        private void AtualizarSegmento()
        {
            segmento.PontosAlterar(pontoOrigem, 0);
            segmento.PontosAlterar(pontoFinal, segmento.PontosListaTamanho - 1);
            segmento.PontosAlterar(Matematica.GerarPtosCirculo(angulo, raio), segmento.PontosListaTamanho - 1);
            segmento.ObjetoAtualizar();
        }
    }
}
