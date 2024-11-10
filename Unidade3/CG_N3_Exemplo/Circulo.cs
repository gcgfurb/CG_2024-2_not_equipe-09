
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
    internal class Circulo : Objeto
    {
        public Circulo(Objeto _paiRef, ref char _rotulo, ref double _raio) : this(_paiRef, ref _rotulo, ref _raio, new Ponto4D(0.5, 0.5))
        {
            
        }

        public Circulo(Objeto _paiRef, ref char _rotulo, ref double _raio, Ponto4D ptoDeslocamento) : base(_paiRef, ref _rotulo)
        {
            PrimitivaTipo = PrimitiveType.Points;
            PrimitivaTamanho = 1;

            base.PontosAdicionar(ptoDeslocamento);
            double angulo = 5;

            for (int i = 0; i < 73; i++) {
                Ponto4D pto = Matematica.GerarPtosCirculo(angulo, 0.5);
                base.PontosAdicionar(pto);
                angulo += 5;
            }
            
            Atualizar(ptoDeslocamento);
        }

        public void Atualizar(Ponto4D ptoDeslocamento)
        {
            base.ObjetoAtualizar();
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