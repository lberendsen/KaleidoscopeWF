using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace KaleidoscopeWF
{
    public partial class frmMaster : Form
    {
        // Variáveis globais que são apenas atualizadas
        Random randomGen = new Random();
        int intHeight;
        int intWidth;
        int intMiddleHeight;
        int intMiddleWidth;
        int defaultStep;
        Graphics g;
        Random randomColor;
        Color defaultColor;
        Pen defaultPen;

        public frmMaster()
        {
            InitializeComponent();

            // O desenho do caledoscópio é colocado dentro de um timer.
            // isso permite maior controle sobre a execução do desenho, 
            // e ainda coloca-o em uma Thread, liberando o SO.

            tmrStep.Tick += tmrStep_Tick;
            tmrStep.Start();
        }

        void tmrStep_Tick(object sender, EventArgs e)
        {
            g = frmMaster.ActiveForm.CreateGraphics();

            // Define uma cor aleatória
            randomColor = new Random();
            defaultColor = Color.FromArgb(randomColor.Next(255), randomColor.Next(255), randomColor.Next(255));
            defaultPen = new Pen(defaultColor, 2);

            // Identifica as dimensões da janela
            intHeight = (int)g.VisibleClipBounds.Height;
            intWidth = (int)g.VisibleClipBounds.Width;

            // Identifica o centro da janela
            intMiddleHeight = intHeight / 2;
            intMiddleWidth = intWidth / 2;

            // Identifica os pontos cardinais
            Point posTopLeft = new Point(0, 0);
            Point posTopRight = new Point(intWidth, 0);
            Point posBottomLeft = new Point(0, intHeight);
            Point posBottomRight = new Point(intWidth, intHeight);
            Point posMiddle = new Point(intMiddleWidth, intMiddleHeight);
            Point posMiddleTop = new Point(intMiddleWidth, 0);
            Point posMiddleLeft = new Point(0, intMiddleHeight);
            Point posMiddleRight = new Point(intWidth, intMiddleHeight);
            Point posMiddleBottom = new Point(intMiddleWidth, intHeight);

            // define um passo aleatório
            defaultStep = randomColor.Next(3, 10);

            // Configura os quadrantes
            SquareSpin q1 = new SquareSpin(posTopLeft, posMiddleTop, posMiddle, posMiddleLeft, defaultStep, posTopLeft, posMiddle, SquareSpin.Direction.clockwise);
            SquareSpin q2 = new SquareSpin(posMiddleTop, posTopRight, posMiddleRight, posMiddle, defaultStep, posTopRight, posMiddle, SquareSpin.Direction.anticlockwise);
            SquareSpin q3 = new SquareSpin(posMiddleLeft, posMiddle, posMiddleBottom, posBottomLeft, defaultStep, posMiddle, posBottomLeft, SquareSpin.Direction.anticlockwise);
            SquareSpin q4 = new SquareSpin(posMiddle, posMiddleRight, posBottomRight, posMiddleBottom, defaultStep, posMiddle, posBottomRight, SquareSpin.Direction.clockwise);

            // Faz o spin horizontal e vertical

            // Loop horizontal
            //TODO: BouncingStep deve receber como parêmtro diretamente as correnadas, ao invés de ser usado como índice 

            SquareSpin b1 = new SquareSpin(0, intMiddleWidth, defaultStep);
            while ( b1.NoSpaceToForward == false)
            {
                g.DrawLine(defaultPen, posTopLeft.X + b1.Actual, posTopLeft.Y, posMiddle.X - b1.Actual, posMiddle.Y);
                g.DrawLine(defaultPen, posTopRight.X - b1.Actual, posTopRight.Y, posMiddle.X + b1.Actual, posMiddle.Y);
                g.DrawLine(defaultPen, posMiddle.X - b1.Actual, posMiddle.Y, posBottomLeft.X + b1.Actual, posBottomLeft.Y);
                g.DrawLine(defaultPen, posMiddle.X + b1.Actual, posMiddle.Y, posBottomRight.X - b1.Actual, posBottomRight.Y);

                b1.StepForward();

            }

            // Loop vertical
            SquareSpin b2 = new SquareSpin(0, intMiddleHeight, defaultStep);
            while( b2.NoSpaceToForward == false)
            {
                g.DrawLine(defaultPen, posMiddleTop.X, posMiddleTop.Y + b2.Actual, posMiddleLeft.X, posMiddleLeft.Y - b2.Actual);
                g.DrawLine(defaultPen, posMiddleTop.X, posMiddleTop.Y + b2.Actual, posMiddleRight.X, posMiddleRight.Y - b2.Actual);
                g.DrawLine(defaultPen, posMiddleRight.X, posMiddleRight.Y + b2.Actual, posMiddleBottom.X, posMiddleBottom.Y - b2.Actual);
                g.DrawLine(defaultPen, posMiddleLeft.X, posMiddleLeft.Y + b2.Actual, posMiddleBottom.X, posMiddleBottom.Y - b2.Actual);

                b2.StepForward();
            }

            Application.DoEvents();
        }

        // Pressionando uma tecla o caledoscópio e a aplicação são interrompidos.
        private void frmMaster_KeyDown(object sender, KeyPressEventArgs e)
        {
            tmrStep.Stop();
            this.Close();
        }

        // Quando redimensiona a janela o caledoscópio para, de forma que possa recalcuar
        // suas dimensões quando o resize terminar.

        private void frmMaster_ResizeBegin(object sender, EventArgs e)
        {
            tmrStep.Stop();
        }

        private void frmMaster_ResizeEnd(object sender, EventArgs e)
        {
            tmrStep.Start();
        }
    }
}
