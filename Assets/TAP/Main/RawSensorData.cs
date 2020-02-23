using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawSensorData 
{
    public enum DataType
    {
        IMU,
        Device
    }

    public static readonly int iIMU_GYRO = 0;
    public static readonly int iIMU_ACCELEROMETER = 1;
    public static readonly int iDEV_THUMB = 0;
    public static readonly int iDEV_INDEX = 1;
    public static readonly int iDEV_MIDDLE = 2;
    public static readonly int iDEV_RING = 3;
    public static readonly int iDEV_PINKY = 4;

    public readonly int timestamp;
    public readonly DataType type;
    public readonly Vector3[] points;

    public readonly string streamedString;

    private RawSensorData(int timestamp, DataType type, Vector3[] points, string streamedString) {
        this.timestamp = timestamp;
        this.type = type;
        this.points = points;
        this.streamedString = streamedString;
    }

    public static RawSensorData makeFromString(string str, string delimeter)
    {
        string[] components = str.Split(delimeter[0]);
        
        if (components.Length < 2)
        {
            return null;
        }

        string timestampString = components[0];

        int timestamp;
        if (!int.TryParse(timestampString, out timestamp))
        {
            return null;
        }

        string typeString = components[1];
        DataType type;
        if (typeString.ToLower().Equals("imu"))
        {
            type = DataType.IMU;
        } else if (typeString.ToLower().Equals("device"))
        {
            type = DataType.Device;
        } else
        {
            return null;
        }

        int pointsLength = 0;
        if (type == DataType.Device)
        {
            if (components.Length < 17)
            {
                return null;
            } else
            {
                pointsLength = 5;
            }
        }
        else
        {
            if (components.Length < 8)
            {
                return null;
            } else
            {
                pointsLength = 2;
            }
        }

        if (pointsLength == 0)
        {
            return null;
        }

        Vector3[] points = new Vector3[pointsLength];
        int i = 2;
        int pointIndex = 0;
        while (i < components.Length)
        {
            double x = 0.0;
            double y = 0.0;
            double z = 0.0;

            if (!double.TryParse(components[i], out x))
            {
                
                return null;
            }
            i++;
            if (i >= components.Length)
            {
                
                return null;
            }
            if (!double.TryParse(components[i], out y))
            {
                return null;
            }
            i++;
            if (i >= components.Length)
            {
                return null;
            }
            if (!double.TryParse(components[i], out z))
            {
                return null;
            }
            i++;
            Vector3 point = new Vector3((float)x, (float)y, (float)z);
            if (pointIndex < points.Length)
            {
                points[pointIndex] = point;
            } else
            {
                return null;
            }
            pointIndex++;
        }

        return new RawSensorData(timestamp, type, points, str);

    }

    public Vector3 GetPoint(int index)
    {
        if (this.points.Length > 0 && index >= 0 && index < this.points.Length)
        {
            return this.points[index];
        } else
        {
            return new Vector3(0,0,0);
        }
    }
}
