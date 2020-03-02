
import axios from 'axios';
// import { fileServer } from './config'
const customRequest = ({
    action,
    data,
    file,
    filename,
    headers,
    onError,
    onProgress,
    onSuccess,
    withCredentials,
}) => {
    var _this = this;
    // EXAMPLE: post form-data with 'axios'
    const formData = new FormData();
    if (data) {
        Object.keys(data).map(key => {
            formData.append(key, data[key]);
        });
    }
    formData.append(filename, file);

    axios
        .post(action, formData, {
            withCredentials,
            headers: {
                'Authorization': "Bearer ZZuLhOFsaPoQmSU8JmNzrForgGaGUwO7dHxlxRpiR8R6Sb8umAw2GPrn3JgkelO6Fy8gzIi1egiqqPBIc+iScL2KsJXN9DsIvQDxCZSqEd2E2q0Igs5dY6meuwA2KNT6MpxDY+0RN7ZXA316rPwJWAg1lYK50dlMQnFzAKDXLCf24MGb92/yE4G2ITEXcJUs"
            },
            onUploadProgress: ({ total, loaded }) => {
                onProgress({ percent: Math.round(loaded / total * 100).toFixed(2) }, file);
            },
        })
        .then(({ data: response }) => {
            // var imageList = _this.state.ImageList;
            // var fileRes = response[0]
            // var url = fileRes.data
            // _this.props.BackImageAdd({
            //     Id: Guid(),
            //     Src: `${config.fileServer}/${url}`
            // })
            // _this.props.AttachmentsNew({
            //     URL: url,
            //     Extension: fileRes.extension,
            //     Size: fileRes.size,
            //     SourceType: 0
            // })
            // _this.props.AttachmentsGetListPaged(1)
            onSuccess(response, file);
        })
        .catch(onError);

    return {
        abort() {
            console.log('upload progress is aborted.');
        },
    };
}


export default customRequest;
