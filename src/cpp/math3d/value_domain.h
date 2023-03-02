#pragma once

#include <algorithm>
//#include <cmath>

namespace vim::math3d
{
    template <typename T = double>
    class ValueDomain {
    public:
        static_assert(std::is_arithmetic<T>::value, "T must be a numerical type");

        const T Lower;
        const T Upper;

        ValueDomain(T lower, T upper): Lower(lower), Upper(upper) {}

        inline T normalize(T value) const { return std::clamp(value, Lower, Upper) / Upper; }
    };
}
