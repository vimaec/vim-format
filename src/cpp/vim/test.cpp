
#include "test-object-model.cpp"

int main(int num, char** args)
{
    auto this_exe_path = normalize_path(std::string(args[0]));
    const auto repo_path = this_exe_path.erase(this_exe_path.rfind("src"));

    const auto object_model_result = test_object_model(repo_path);

    if (object_model_result != 0)
        return object_model_result;

    //TODO: test G3D here

    return 0;
}