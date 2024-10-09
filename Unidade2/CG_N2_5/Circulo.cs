
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
    internal class Circulo : Objeto
    {
        private double raio;
        private Ponto4D centro;
        public Circulo(Objeto _paiRef, ref char _rotulo, ref double _raio, Ponto4D ptoDeslocamento, double angulo) : base(_paiRef, ref _rotulo)
        {
            raio = _raio;
            centro = ptoDeslocamento;
            PrimitivaTipo = PrimitiveType.Points;
            PrimitivaTamanho = 1;

            // Criação do círculo
            base.PontosAdicionar(ptoDeslocamento);
            double anguloOriginal = angulo;

            for (int i = 0; i < 1300; i++) {
                Ponto4D pto = Matematica.GerarPtosCirculo(angulo, _raio);
                Ponto4D pontoFinal = new Ponto4D(pto.X + ptoDeslocamento.X, pto.Y + ptoDeslocamento.Y, 0);
                base.PontosAdicionar(pontoFinal);
                angulo += anguloOriginal;
            }

            Atualizar(ptoDeslocamento);
        }

        public void Atualizar(Ponto4D ptoDeslocamento)
        {
            centro = ptoDeslocamento;
            base.ObjetoAtualizar();
        }

        public Ponto4D GetCentro()
        {
            return centro;
        }

        public double GetRaio()
        {
            return raio;
        }

        public void MoverCirculoMenor(Circulo circuloMaior, char direcao)
        {
            Ponto4D centroMenor = this.GetCentro();
            Ponto4D centroMaior = circuloMaior.GetCentro();
            double raioMaior = circuloMaior.GetRaio();
            Ponto4D pontoCentroMenor = new Ponto4D(centroMenor.X, centroMenor.Y);
            double novoCentroMenorY = centroMenor.Y;
            double novoCentroMenorX = centroMenor.X;

            double delta = 0.01; // Valor de deslocamento

            // Movimentos
            switch (direcao)
            {
                case 'C':  // Cima
                    novoCentroMenorY = centroMenor.Y + delta;
                    pontoCentroMenor = new Ponto4D(centroMenor.X, novoCentroMenorY);
                    break;
                case 'B':  // Baixo
                    novoCentroMenorY = centroMenor.Y - delta;
                    pontoCentroMenor = new Ponto4D(centroMenor.X, novoCentroMenorY);
                    break;
                case 'E':  // Esquerda
                    novoCentroMenorX = centroMenor.X - delta;
                    pontoCentroMenor = new Ponto4D(centroMenor.X, novoCentroMenorX);
                    break;
                case 'D':  // Direita
                    novoCentroMenorX = centroMenor.X + delta;
                    pontoCentroMenor = new Ponto4D(centroMenor.X, novoCentroMenorX);
                    break;
            }

            // Calcular distância entre os centros
            // Ponto4D pontoCentroMenor = new Ponto4D(centroMenor.X, centroMenor.Y);
            Ponto4D pontoCentroMaior = new Ponto4D(centroMaior.X, centroMaior.Y);
            double distancia = Matematica.DistanciaQuadrado(pontoCentroMenor, pontoCentroMaior);

            // Verifica se o centro do círculo menor está dentro do círculo maior (distância^2 <= raioMaior^2)
            if (distancia <= raioMaior * raioMaior)
            {
                pontosLista.Clear();
                
                centroMenor.Y = novoCentroMenorY;
                centroMenor.X = novoCentroMenorX;

                base.PontosAdicionar(centroMenor);
                int angulo = 1;

                for (int i = 0; i < 1300; i++) {
                    Ponto4D pto = Matematica.GerarPtosCirculo(angulo, 0.1);
                    Ponto4D pontoFinal = new Ponto4D(pto.X + centroMenor.X, pto.Y + centroMenor.Y, 0);
                    base.PontosAdicionar(pontoFinal);
                    angulo += 1;
                }
                this.Atualizar(centroMenor);  // Atualiza a posição do círculo menor
            }
        }

#if CG_Debug
        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Circulo _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
            retorno += base.ImprimeToString();
            return retorno;
        }
#endif
    }
}