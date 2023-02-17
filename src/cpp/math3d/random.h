#pragma once

#include "hash.h"
#include "structs.h"

namespace vim::math3d::statelessRandom {
    template <typename T = std::size_t>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        random(int index, int seed = 0) { return (T)hash::combine(seed, index); }

    template <typename T = float>
    inline T randomNumb(T min, T max, int index, int seed) {
        return (T)random<std::size_t>(index, seed) / std::numeric_limits<size_t>::max() * (max - min) + min;
    }

    template <typename T = float>
    inline T randomNumb(int index, int seed = 0) { return randomNumb<T>(0, 1, index, seed); }

    template <typename T = float>
    inline Vector2<T> randomVector2(int index, int seed = 0) {
        return Vector2<T>(randomNumb<T>(index * 2, seed), randomNumb<T>(index * 2 + 1, seed));
    }

    template <typename T = float>
    inline Vector3<T> randomVector3(int index, int seed = 0) {
        return Vector3(randomNumb<T>(index * 3, seed), randomNumb<T>(index * 3 + 1, seed), randomNumb<T>(index * 3 + 2, seed));
    }

    template <typename T = float>
    inline Vector4<T> randomVector4(int index, int seed = 0) {
        return Vector4(randomNumb<T>(index * 4, seed), randomNumb<T>(index * 4 + 1, seed), randomNumb<T>(index * 4 + 2, seed), randomNumb<T>(index * 4 + 3, seed));
    }
}