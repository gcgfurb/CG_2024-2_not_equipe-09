
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
    internal class Circulo : Objeto
    {
        private double raio;
        private Ponto4D centro;
        private double novoCentroMenorY;
        private double novoCentroMenorX;
        private bool foraRetangulo = false;
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
            double raioMaior = circuloMaior.GetRaio();
            Ponto4D centroMenor = this.GetCentro();
            novoCentroMenorX = centroMenor.X;
            novoCentroMenorY = centroMenor.Y;

            Ponto4D pontoRetangulo1 = Matematica.GerarPtosCirculo(45, raioMaior);
            Ponto4D pontoFinalRetangulo1 = new Ponto4D(pontoRetangulo1.X + raioMaior, pontoRetangulo1.Y + raioMaior, 0);
            Ponto4D pontoRetangulo2 = Matematica.GerarPtosCirculo(225, raioMaior);
            Ponto4D pontoFinalRetangulo2 = new Ponto4D(pontoRetangulo2.X + raioMaior, pontoRetangulo2.Y + raioMaior, 0);

            Ponto4D centroMaior = circuloMaior.GetCentro();
            Ponto4D pontoCentroMenor = new Ponto4D(centroMenor.X, centroMenor.Y);

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
                    pontoCentroMenor = new Ponto4D(novoCentroMenorX, centroMenor.Y);
                    break;
                case 'D':  // Direita
                    novoCentroMenorX = centroMenor.X + delta;
                    pontoCentroMenor = new Ponto4D(novoCentroMenorX, centroMenor.Y);
                    break;
            }

            if (
                pontoCentroMenor.X > pontoFinalRetangulo2.X &&
                pontoCentroMenor.X < pontoFinalRetangulo1.X &&
                pontoCentroMenor.Y > pontoFinalRetangulo2.Y &&
                pontoCentroMenor.Y < pontoFinalRetangulo1.Y &&
                foraRetangulo == false
            ) {
                MovePontoCentro();
            } else {
                // Calcular distância entre os centros
                Ponto4D pontoCentroMaior = new Ponto4D(centroMaior.X, centroMaior.Y);
                double distancia = Matematica.DistanciaQuadrado(pontoCentroMenor, pontoCentroMaior);

                // Verifica se o centro do círculo menor está dentro do círculo maior (distância^2 <= raioMaior^2)
                if (distancia <= raioMaior * raioMaior)
                {
                    MovePontoCentro();
                }

                foraRetangulo = true;
            }
        }

        private void MovePontoCentro()
        {
            pontosLista.Clear();
            Ponto4D centroMenor = this.GetCentro();
                
            centroMenor.Y = this.novoCentroMenorY;
            centroMenor.X = this.novoCentroMenorX;

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