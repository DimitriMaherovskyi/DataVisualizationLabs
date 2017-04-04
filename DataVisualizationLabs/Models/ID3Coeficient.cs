using System;

namespace Models
{
    public class ID3Coeficient : IComparable
    {
        public ID3Coeficient() { }

        public ID3Coeficient(double coeficient, int samplecount)
        {
            Coeficient = coeficient;
            SampleCount = samplecount;
        }

        public double Coeficient { get; set; }
        public int SampleCount { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is ID3Coeficient)
            {
                var id3 = (ID3Coeficient)obj;
                if (Coeficient < id3.Coeficient)
                {
                    return -1;
                }
                if (Coeficient > id3.Coeficient)
                {
                    return 1;
                }
                if (Coeficient == id3.Coeficient)
                {
                    if (SampleCount > id3.SampleCount)
                    {
                        return 1;
                    }
                    if (SampleCount < id3.SampleCount)
                    {
                        return -1;
                    }
                }

                return 0;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
