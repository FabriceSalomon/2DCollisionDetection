﻿using _2DCollisionLibrary.Models;
using _2DCollisionLibrary.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _2DCollisionLibrary.Helpers
{
    public static class GeometryHelper
    {
        public static void MoveTo(this IEnumerable<Vertex> vertices, double xPos, double yPos)
        {
            BoundingBox boundingBox = new BoundingBox();
            boundingBox.UpdateBoundingBox(vertices.ToArray());
            double xOffset = xPos - boundingBox.Rect.TopLeft.X;
            double yOffset = yPos - boundingBox.Rect.TopLeft.Y;
            foreach (var vertex in vertices)
            {
                vertex.Move(xOffset, yOffset);
            }
        }

        public static void Rotate(this IEnumerable<Vertex> vertices, Point transformOrigin, double fromAngle, double toAngle)
        {
            double rotateTo = toAngle - fromAngle;
            foreach (var vertex in vertices)
            {
                vertex.Rotate(transformOrigin, rotateTo);
            }
        }

        public static void Scale(this IEnumerable<Vertex> vertices, double xScale, double yScale, double centerX, double centerY)
        {
            BoundingBox boundingBox = new BoundingBox();
            boundingBox.UpdateBoundingBox(vertices.ToArray());
            Point origin = new Point(boundingBox.Rect.Left + (boundingBox.Rect.Width * centerX), boundingBox.Rect.Top + (boundingBox.Rect.Height * centerY));
            foreach (var vertex in vertices)
            {
                Point direction = (Point)Point.Subtract(origin, vertex.Position.Point);
                vertex.Move(direction.X * (1 - xScale), direction.Y * (1 - yScale));
            }
        }
    }
}
