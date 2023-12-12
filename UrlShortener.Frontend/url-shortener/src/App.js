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
    <div
      style={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        height: "100vh",
      }}
    >
      <TextField
        inputRef={textInputRef}
        label="Type url..."
        variant="outlined"
        style={{ marginBottom: "10px", width: "300px" }}
      />
      <br />
      <div
        style={{ position: "relative", width: "300px", marginBottom: "10px" }}
      >
        <TextField
          value={readOnlyText}
          readOnly
          rows={4}
          fullWidth
          style={{ width: "300px" }}
        />
        <IconButton
          onClick={handleCopyClick}
          style={{ position: "absolute", top: "5px", right: "5px" }}
        >
          <ContentCopyIcon />
        </IconButton>
      </div>
      <br />
      <Button
        variant="contained"
        onClick={handleButtonClick}
        style={{ width: "300px" }}
      >
        Create shortcut
      </Button>
    </div>
  );
};

export default App;
