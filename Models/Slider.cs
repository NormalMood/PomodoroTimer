using System;
using System.Collections.Generic;

namespace Pomodoro_Timer.Models
{
    public class Slider : Base.BasePropertyChanged
    {
        private const float _sliderCenterCoord_x_y = 100;
        private const float _sliderCircleRadius = 100;
        private struct ArcSegmentEndPointsCoords
        {
            public float X;
            public float Y;
        }
        private class CoordsCalculator
        {
            private ulong _amountOfSeconds; //amount of seconds
            private float _oneSectorSize; // sector size in degrees
            private const float _degreesInCircle = 360f;
            private const float _lastEndPointXCoord = 99.99f;
            private const float _lastEndPointYCoord = 0f;
            public CoordsCalculator(ulong amountOfSeconds)
            {
                _amountOfSeconds = amountOfSeconds;
                _oneSectorSize = _degreesInCircle / amountOfSeconds;
            }
            private ArcSegmentEndPointsCoords GetCoordsOfPoint(float currentSectorSizeInDegrees)
            {
                ArcSegmentEndPointsCoords coords = new ArcSegmentEndPointsCoords();
                double aParameter = currentSectorSizeInDegrees * Math.PI / 180;
                //formulas
                coords.X = (float)Math.Round(_sliderCenterCoord_x_y + _sliderCircleRadius * Math.Sin(aParameter), 3);
                coords.Y = (float)Math.Round(_sliderCenterCoord_x_y + _sliderCircleRadius * Math.Sin(aParameter + 3 * Math.PI / 2), 3);
                //////////////////////////////////
                return coords;
            }
            public ArcSegmentEndPointsCoords[] GetAllCoords()
            {
                var tempListForCoords = new List<ArcSegmentEndPointsCoords>();
                float newValueOfSector = _oneSectorSize;
                for (uint i = 0; i < _amountOfSeconds - 1; i++)
                {
                    tempListForCoords.Add(GetCoordsOfPoint(newValueOfSector));
                    newValueOfSector += _oneSectorSize;
                }
                //last coords are always constants
                tempListForCoords.Add(new ArcSegmentEndPointsCoords { X = _lastEndPointXCoord, Y = _lastEndPointYCoord });
                return tempListForCoords.ToArray();
            }
        }
        /*=================================================*/
        private const string _movingForwardSliderColor = "#3073FF";
        private const string _movingBackwardSliderColor = "Red";
        private string _sliderColor = _movingForwardSliderColor;
        public string SliderColor
        {
            get
            {
                return _sliderColor;
            }
            set
            {
                _sliderColor = value;
                OnPropertyChanged();
            }
        }
        private float _endX_Arc1;
        public float EndX_Arc1 { get { return _endX_Arc1; } set { _endX_Arc1 = value; OnPropertyChanged(); } }
        private float _endY_Arc1;
        public float EndY_Arc1 { get { return _endY_Arc1; } set { _endY_Arc1 = value; OnPropertyChanged(); } }

        private float _endX_Arc2;
        public float EndX_Arc2 { get { return _endX_Arc2; } set { _endX_Arc2 = value; OnPropertyChanged(); } }
        private float _endY_Arc2;
        public float EndY_Arc2 { get { return _endY_Arc2; } set { _endY_Arc2 = value; OnPropertyChanged(); } }
        private float _endX_Arc3;
        public float EndX_Arc3 { get { return _endX_Arc3; } set { _endX_Arc3 = value; OnPropertyChanged(); } }
        private float _endY_Arc3;
        public float EndY_Arc3 { get { return _endY_Arc3; } set { _endY_Arc3 = value; OnPropertyChanged(); } }
        private float _endX_Arc4;
        public float EndX_Arc4 { get { return _endX_Arc4; } set { _endX_Arc4 = value; OnPropertyChanged(); } }
        private float _endY_Arc4;
        public float EndY_Arc4 { get { return _endY_Arc4; } set { _endY_Arc4 = value; OnPropertyChanged(); } }
        /*=================================================*/

        public void CalculateSlidersCoordsForCurrentTimer(ulong milliSeconds)
        {
            CoordsCalculator coordsCalculator = new CoordsCalculator(amountOfSeconds: milliSeconds / 1000);
            _slidersCoordsForCurrentTimer = coordsCalculator.GetAllCoords();
            _indexInArrayForCoords = _initIndexInArrayForCoords;
        }
        public void SetSlidersCoordsForReverse()
        {
            _slidersCoordsForCurrentTimer = new ArcSegmentEndPointsCoords[]
            {
                new ArcSegmentEndPointsCoords() { X = 61.732f, Y = 7.612f },
                new ArcSegmentEndPointsCoords() { X = 29.289f, Y=29.289f },
                new ArcSegmentEndPointsCoords() { X = 7.612f, Y = 61.732f },
                new ArcSegmentEndPointsCoords() { X = 0, Y = 99.99f },
                new ArcSegmentEndPointsCoords() { X = 7.612f, Y = 138.268f },
                new ArcSegmentEndPointsCoords() { X = 29.289f, Y = 170.711f },
                new ArcSegmentEndPointsCoords() { X = 61.732f, Y = 192.388f },
                new ArcSegmentEndPointsCoords() { X = 99.99f, Y = 200 },
                new ArcSegmentEndPointsCoords() { X = 138.268f, Y = 192.388f },
                new ArcSegmentEndPointsCoords() { X = 170.711f, Y = 170.711f },
                new ArcSegmentEndPointsCoords() { X = 192.388f, Y = 138.268f },
                new ArcSegmentEndPointsCoords() { X = 200, Y = 100.01f },
                new ArcSegmentEndPointsCoords() { X = 192.388f, Y = 61.732f },
                new ArcSegmentEndPointsCoords() { X = 170.711f, Y = 29.289f },
                new ArcSegmentEndPointsCoords() { X = 138.268f, Y = 7.612f },
                new ArcSegmentEndPointsCoords() { X = 100.01f, Y = 0 }
            };
            _indexInArrayForCoords = _initIndexInArrayForCoords;
        }
        private ArcSegmentEndPointsCoords[] _slidersCoordsForCurrentTimer;
        private const short _initIndexInArrayForCoords = -1;
        private int _indexInArrayForCoords;
        /*-----------------------------------------------------*/
        private const byte _endX_Arc1_EndBorderForShowing = 200;
        private const byte _endY_Arc1_EndBorderForShowing = 100;
        private const byte _endX_Arc2_EndBorderForShowing = 100;
        private const byte _endY_Arc2_EndBorderForShowing = 200;
        private const byte _endX_Arc3_EndBorderForShowing = 0;
        private const byte _endY_Arc3_EndBorderForShowing = 100;
        private const float _endX_Arc4_EndBorderForShowing = 99.99f;
        private const byte _endY_Arc4_EndBorderForShowing = 0;
        /*-----------------------------------------------------*/
        private const byte _endX_Arc1_EndBorderForHiding = 100;
        private const byte _endY_Arc1_EndBorderForHiding = 0;
        private const byte _endX_Arc2_EndBorderForHiding = 200;
        private const byte _endY_Arc2_EndBorderForHiding = 100;
        private const byte _endX_Arc3_EndBorderForHiding = 100;
        private const byte _endY_Arc3_EndBorderForHiding = 200;
        private const float _endX_Arc4_EndBorderForHiding = 0;
        private const byte _endY_Arc4_EndBorderForHiding = 100;
        /*-----------------------------------------------------*/
        public void SetCoordsForSlider()
        {
            _indexInArrayForCoords++;
            if (_slidersCoordsForCurrentTimer[_indexInArrayForCoords].X > 100 && 
                _slidersCoordsForCurrentTimer[_indexInArrayForCoords].Y <= 100)
            {
                EndY_Arc1 = _slidersCoordsForCurrentTimer[_indexInArrayForCoords].Y;
                EndX_Arc1 = _slidersCoordsForCurrentTimer[_indexInArrayForCoords].X;
            }
            else if (_slidersCoordsForCurrentTimer[_indexInArrayForCoords].X >= 100 && 
                     _slidersCoordsForCurrentTimer[_indexInArrayForCoords].Y <= 200)
            {
                if (_indexInArrayForCoords - 1 != -1)
                    if (_slidersCoordsForCurrentTimer[_indexInArrayForCoords - 1].X < 200 && 
                        _slidersCoordsForCurrentTimer[_indexInArrayForCoords - 1].Y < 100)
                    {
                        EndY_Arc1 = _endY_Arc1_EndBorderForShowing;
                        EndX_Arc1 = _endX_Arc1_EndBorderForShowing;
                    }
                if (_indexInArrayForCoords == 0)
                {
                    EndY_Arc1 = _endY_Arc1_EndBorderForShowing;
                    EndX_Arc1 = _endX_Arc1_EndBorderForShowing;
                }
                EndY_Arc2 = _slidersCoordsForCurrentTimer[_indexInArrayForCoords].Y;
                EndX_Arc2 = _slidersCoordsForCurrentTimer[_indexInArrayForCoords].X;
            }
            else if (_slidersCoordsForCurrentTimer[_indexInArrayForCoords].X < 100 && 
                     _slidersCoordsForCurrentTimer[_indexInArrayForCoords].Y >= 100)
            {
                if (_indexInArrayForCoords - 1 != -1)
                    if (_slidersCoordsForCurrentTimer[_indexInArrayForCoords - 1].X > 100 && 
                        _slidersCoordsForCurrentTimer[_indexInArrayForCoords - 1].Y < 200)
                    {
                        EndY_Arc2 = _endY_Arc2_EndBorderForShowing;
                        EndX_Arc2 = _endX_Arc2_EndBorderForShowing;
                    }
                
                EndY_Arc3 = _slidersCoordsForCurrentTimer[_indexInArrayForCoords].Y;
                EndX_Arc3 = _slidersCoordsForCurrentTimer[_indexInArrayForCoords].X;
            }
            else
            {
                if (_indexInArrayForCoords - 1 != -1)
                    if (_slidersCoordsForCurrentTimer[_indexInArrayForCoords - 1].X < 100 &&
                        _slidersCoordsForCurrentTimer[_indexInArrayForCoords - 1].Y > 100)
                    {
                        EndY_Arc3 = _endY_Arc3_EndBorderForShowing;
                        EndX_Arc3 = _endX_Arc3_EndBorderForShowing;
                    }
                if (_indexInArrayForCoords == 2)
                {
                    EndY_Arc3 = _endY_Arc3_EndBorderForShowing;
                    EndX_Arc3 = _endX_Arc3_EndBorderForShowing;
                }
                EndY_Arc4 = _slidersCoordsForCurrentTimer[_indexInArrayForCoords].Y;
                EndX_Arc4 = _slidersCoordsForCurrentTimer[_indexInArrayForCoords].X;
            }
        }
        public void SetCoordsForAllArcs()
        {
            EndY_Arc1 = _endY_Arc1_EndBorderForShowing;
            EndX_Arc1 = _endX_Arc1_EndBorderForShowing;
            EndY_Arc2 = _endY_Arc2_EndBorderForShowing;
            EndX_Arc2 = _endX_Arc2_EndBorderForShowing;
            EndY_Arc3 = _endY_Arc3_EndBorderForShowing;
            EndX_Arc3 = _endX_Arc3_EndBorderForShowing;
            EndY_Arc4 = _endY_Arc4_EndBorderForShowing;
            EndX_Arc4 = _endX_Arc4_EndBorderForShowing;
        }
        public void HideSlider()
        {
            EndY_Arc1 = _endY_Arc1_EndBorderForHiding;
            EndX_Arc1 = _endX_Arc1_EndBorderForHiding;

            EndY_Arc2 = _endY_Arc2_EndBorderForHiding;
            EndX_Arc2 = _endX_Arc2_EndBorderForHiding;

            EndY_Arc3 = _endY_Arc3_EndBorderForHiding;
            EndX_Arc3 = _endX_Arc3_EndBorderForHiding;

            EndY_Arc4 = _endY_Arc4_EndBorderForHiding;
            EndX_Arc4 = _endX_Arc4_EndBorderForHiding;
        }
        public void SetColorForMovingForwardSlider()
        {
            SliderColor = _movingForwardSliderColor;
        }
        public void SetColorForMovingBackwardSlider()
        {
            SliderColor = _movingBackwardSliderColor;
        }
    }
}
