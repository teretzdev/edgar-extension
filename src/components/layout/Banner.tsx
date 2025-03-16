import React from "react";

type BannerProps = {
  title: string;
  subtitle?: string;
  backgroundImage?: string;
};

const Banner: React.FC<BannerProps> = ({ title, subtitle, backgroundImage }) => {
  return (
    <div
      className="relative w-full h-64 flex items-center justify-center bg-gray-800 text-white"
      style={{
        backgroundImage: backgroundImage ? `url(${backgroundImage})` : undefined,
        backgroundSize: "cover",
        backgroundPosition: "center",
      }}
    >
      <div className="absolute inset-0 bg-black bg-opacity-50"></div>
      <div className="relative text-center">
        <h1 className="text-4xl font-bold">{title}</h1>
        {subtitle && <p className="text-lg mt-2">{subtitle}</p>}
      </div>
    </div>
  );
};

export default Banner;
