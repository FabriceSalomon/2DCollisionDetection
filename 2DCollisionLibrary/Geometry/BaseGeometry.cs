﻿using _2DCollisionLibrary.Interfaces;
using _2DCollisionLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using _2DCollisionLibrary.Models;
using _2DCollisionLibrary.Objects;
using _2DCollisionLibrary.Adapters;

namespace _2DCollisionLibrary.Geometry
{
    public abstract class BaseGeometry : BoundingBox, IGeometry
    {
        public string Name { get; set; }
        public Point TransformOrigin { get; set; }
        public Position Position { get; set; }
        public Transformation Transformation { get; set; }
        public Vertex[] Vertices { get; private set; }
        public Action GeometryChanged { get; set; }

        public BaseGeometry()
        {
            Name = "";
            Position = new Position();
            Vertices = new Vertex[0];
            TransformOrigin = new Point();
            Transformation = new Transformation();
        }

        public abstract Vertex[] GetVertices();
        public abstract void Refresh();

        public void Build()
        {
            Vertices = GetVertices();
            UpdateBoundingBox(Vertices);
            Position.Point = new Point(Rect.Center().X, Rect.Center().Y);

            if (GeometryChanged != null)
                GeometryChanged();
        }

        public virtual void MoveOffset(double xOffset, double yOffset)
        {
            double xPos = Position.X + xOffset;
            double yPos = Position.Y + yOffset;
            MoveTo(xPos, yPos);
        }

        public virtual void MoveTo(double xPos, double yPos)
        {
            Vertices.MoveTo(xPos, yPos);
            Position.Point = new Point(xPos, yPos);
            UpdateBoundingBox(Vertices);
        }

        public virtual void Rotate(double angle)
        {
            Vertices.Rotate(TransformOrigin, Transformation.Rotation, angle);
            Transformation.Rotation = angle;
            UpdateBoundingBox(Vertices);
        }

        public virtual void Scale(double xScale, double yScale, double centerX, double centerY)
        {
            double xPercent = xScale / Transformation.ScaleX;
            double yPercent = yScale / Transformation.ScaleY;
            Vertices.Scale(xPercent, yPercent, centerX, centerY);
            Transformation.ScaleX = xScale;
            Transformation.ScaleY = yScale;
            UpdateBoundingBox(Vertices);
        }
    }
}
