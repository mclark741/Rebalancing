import React from "react";

/**
 * Component to handle file upload. Works for image
 * uploads, but can be edited to work for any file.
 */
const TransactionsUpload = () => {
  // State to store uploaded file
  const [file, setFile] = React.useState("");

  const handleChange = (event) => {
    setFile(event.target.files[0]);
  };
  // Handles file upload event and updates state
  const handleUpload = () => {
    const apiUrl = `https://localhost:65402/api/Transaction/File`;

    // Create an object of formData
    const formData = new FormData();

    // Update the formData object
    formData.append("file", file, file.name);

    // Details of the uploaded file
    console.log(formData);

    fetch(apiUrl, {
      method: "POST",
      body: formData,
    })
      .then((res) => res.json())
      .then((data) => {
        console.log(data);
      });
  };

  return (
    <div id="upload-box">
      <label htmlFor="file">
        Choose file to upload:
        <input type="file" onChange={handleChange} />
      </label>
      <button onClick={handleUpload}>Upload!</button>
      <p>Filename: {file.name}</p>
      <p>File type: {file.type}</p>
      <p>File size: {file.size} bytes</p>
      {file && <ImageThumb image={file} />}
    </div>
  );
};

/**
 * Component to display thumbnail of image.
 */
const ImageThumb = ({ image }) => {
  return <img src={URL.createObjectURL(image)} alt={image.name} />;
};

export default TransactionsUpload;
