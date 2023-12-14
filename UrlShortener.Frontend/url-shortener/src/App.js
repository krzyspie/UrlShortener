import React, { useState, useRef } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import IconButton from "@mui/material/IconButton";
import ContentCopyIcon from "@mui/icons-material/ContentCopyOutlined";

const App = () => {
  const [readOnlyText, setReadOnlyText] = useState("");
  const textInputRef = useRef(null);
  const textReadOnlyInputRef = useRef(null);

  const handleButtonClick = async () => {
    try {
      const url = textInputRef.current.value;
      const response = await fetch(
        `https://localhost:7219/shorturl?url=${url}`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      const data = await response.json();

      setReadOnlyText(data);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  const handleCopyClick = async () => {
    try {
      await navigator.clipboard.writeText(textReadOnlyInputRef.current.value);
      console.log("Text copied to clipboard");
    } catch (err) {
      console.error("Unable to copy text to clipboard", err);
    }
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
          inputRef={textReadOnlyInputRef}
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
