#pragma once

namespace vim::math3d::constants {
    const float tolerance = 0.0000001f;

    const double pi = 3.14159265358979323846;
    const double halfPi = pi / 2.0;
    const double twoPi = pi * 2.0;
    const double log10E = 0.434294481903251827651;
    const double log2E = 1.44269504088896340736;
    const double e = 2.71828182845904523536;

    const double radiansToDegrees = 57.295779513082320876798154814105;
    const double degreesToRadians = 0.017453292519943295769236907684886;
    const double oneTenthOfADegree = degreesToRadians / 10.0;

    // TODO: BUG: these two values are inverted dumb-dumb
    const double mmToFeet = 0.00328084;
    const double feetToMm = 1.0 / mmToFeet;
};
