import React, { useState, useRef } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import IconButton from "@mui/material/IconButton";
import ContentCopyIcon from "@mui/icons-material/ContentCopyOutlined";

const App = () => {
  const [readOnlyText, setReadOnlyText] = useState("");
  const textInputRef = useRef(null);

  const handleButtonClick = () => {
    setReadOnlyText("Url shortcut");
  };

  const handleCopyClick = () => {
    textInputRef.current.select();
    document.execCommand("copy");
  };

  return (
    <div style={{ textAlign: "center", marginTop: "50px" }}>
      <TextField
        inputRef={textInputRef}
        label="Type url..."
        variant="outlined"
        style={{ marginBottom: "10px" }}
      />
      <br />
      <TextField
        value={readOnlyText}
        readOnly
        rows={4}
        style={{ width: "300px", marginBottom: "10px" }}
      />
      <br />
      <Button variant="contained" onClick={handleButtonClick}>
        Create shortcut
      </Button>
      <IconButton
        onClick={handleCopyClick}
        style={{ float: "right", marginTop: "-30px", marginRight: "5px" }}
      >
        <ContentCopyIcon />
      </IconButton>
    </div>
  );
};

export default App;
