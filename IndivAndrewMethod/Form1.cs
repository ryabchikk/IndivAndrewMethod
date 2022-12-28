namespace IndivAndrewMethod
{
    public partial class Form1 : Form
    {        
        private Graphics canvas;
        
        SolidBrush brush = new SolidBrush(Color.White);
        
        Pen pen = new Pen(Color.White, 3);
        
        List<Point> points = new List<Point>();
        
        public Form1()
        {
            InitializeComponent();
            
            canvas = pictureBox1.CreateGraphics();
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            List<Point> hull = new List<Point>();
            
            Point pointInLine = points.Where(p => p.X == points.Min(min => min.X)).First();
            Point firstPoint;
            
            do
            {
                hull.Add(pointInLine);
                firstPoint = points[0];

                for (int i = 1; i < points.Count; i++)
                {
                    if ((pointInLine == firstPoint) || (Orientation(pointInLine, firstPoint, points[i]) == -1)) {
                        firstPoint = points[i];
                    }
                }
                
                canvas.DrawLine(pen, pointInLine, firstPoint);
                
                Thread.Sleep(100);
                
                pointInLine = firstPoint;
            }
            while (firstPoint != hull[0]);
        }
        private static int Orientation(Point p1, Point p2, Point p)
        {
            int orin = (p2.X - p1.X) * (p.Y - p1.Y) - (p.X - p1.X) * (p2.Y - p1.Y);
            
            if(orin == 0) {
                return 0;
            }
            else if (orin > 0) {
                return -1;
            }
            else { 
                return 1;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(1300, 900);
            points.Clear();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            canvas.FillEllipse(brush, e.X - 2, e.Y - 2, 4, 4);
            points.Add(e.Location);
        }
    }
}