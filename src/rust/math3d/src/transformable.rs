use num_traits::{Float, FloatConst};
use crate::{Vector2,Vector3,Matrix4x4, Quaternion, Plane};

pub trait Points<T: Float> {
    fn num_points(&self) -> usize;
    fn get_point(&self, n: usize) -> Vector3<T>;
}

pub trait Points2D<T: Float> {
    fn num_points(&self) -> usize;
    fn get_point(&self, n: usize) -> Vector2<T>;
}

pub trait Mappable<TPart> {
    type Container;
    
    fn map<F>(self, f: F) -> Self::Container
    where
        F: Fn(TPart) -> TPart;
}

pub trait Transformable3D<T: Float + FloatConst> {
    type Output: Transformable3D<T, Output = Self::Output>;

    fn transform(&self, mat: Matrix4x4<T>) -> Self::Output;

    fn transform_multiple(&self, matrices: &[Matrix4x4<T>]) -> Self::Output {
        Self::transform(self, Matrix4x4::<T>::multiply(matrices))
    }

    fn translate(&self, offset: Vector3<T>) -> Self::Output {
        self.transform(Matrix4x4::<T>::create_translation(offset))
    }

    fn translate_xyz(&self, x: T, y: T, z: T) -> Self::Output {
        self.translate(Vector3::new(x, y, z))
    }

    fn rotate(&self, q: Quaternion<T>) -> Self::Output {
        self.transform(Matrix4x4::create_rotation(q))
    }

    fn scale(&self, scale: T) -> Self::Output {
        self.scale_vector(Vector3::new(scale, scale, scale))
    }

    fn scale_vector(&self, scales: Vector3<T>) -> Self::Output {
        self.transform(Matrix4x4::create_scale(scales))
    }

    fn scale_xyz(&self, x: T, y: T, z: T) -> Self::Output {
        self.scale_vector(Vector3::new(x, y, z))
    }

    fn scale_x(&self, x: T) -> Self::Output {
        self.scale_xyz(x, T::zero(), T::zero())
    }

    fn scale_y(&self, y: T) -> Self::Output {
        self.scale_xyz(T::zero(), y, T::zero())
    }

    fn scale_z(&self, z: T) -> Self::Output {
        self.scale_xyz(T::zero(), T::zero(), z)
    }

    fn look_at(&self, camera_position: Vector3<T>, camera_target: Vector3<T>, camera_up_vector: Vector3<T>) -> Self::Output {
        self.transform(Matrix4x4::create_look_at(camera_position, camera_target, camera_up_vector))
    }

    fn rotate_around(&self, axis: Vector3<T>, angle: T) -> Self::Output {
        self.transform(Matrix4x4::create_from_axis_angle(axis, angle))
    }

    fn rotate_yaw_pitch_roll(&self, yaw: T, pitch: T, roll: T) -> Self::Output {
        self.transform(Matrix4x4::create_from_yaw_pitch_roll(yaw, pitch, roll))
    }

    fn reflect(&self, plane: Plane<T>) -> Self::Output {
        self.transform(Matrix4x4::create_reflection(plane))
    }

    fn rotate_x(&self, angle: T) -> Self::Output {
        self.rotate_around(Vector3::unit_x(), angle)
    }

    fn rotate_y(&self, angle: T) -> Self::Output {
        self.rotate_around(Vector3::unit_y(), angle)
    }

    fn rotate_z(&self, angle: T) -> Self::Output {
        self.rotate_around(Vector3::unit_z(), angle)
    }

    fn translate_rotate_scale(&self, pos: Vector3<T>, rot: Quaternion<T>, scale: Vector3<T>) -> Self::Output {
        self.translate(pos).rotate(rot).scale_vector(scale)
    }
}

 