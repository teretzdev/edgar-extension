// Update this page (the content is just a fallback if you fail to update the page). Always include w-full and min-h-screen classes in the main element.
const Index = () => {
  return (
    <main className="w-full min-h-screen flex flex-col items-center justify-center bg-gray-100">
      <div className="container mx-auto px-4 py-8">
        <div className="text-center mb-8">
          <h1 className="text-5xl font-extrabold mb-4">Edgar and Friends</h1>
          <p className="text-lg text-gray-700">
            Discover the ultimate solution for room template management and procedural generation.
          </p>
        </div>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
          <div className="bg-white shadow-md rounded-lg p-6">
            <h2 className="text-2xl font-bold mb-4">Feature Highlights</h2>
            <ul className="list-disc list-inside text-gray-600">
              <li>Room Template Management</li>
              <li>LLM Integration</li>
              <li>Edgar Compatibility</li>
              <li>Custom Asset Database</li>
            </ul>
          </div>
          <div className="bg-white shadow-md rounded-lg p-6">
            <h2 className="text-2xl font-bold mb-4">Why Choose Us?</h2>
            <p className="text-gray-600">
              Streamline your workflow with advanced tools and seamless integration.
            </p>
          </div>
        </div>
        <div className="text-center mt-8">
          <button className="bg-blue-500 text-white px-6 py-3 rounded-lg shadow-md hover:bg-blue-600">
            Get Started
          </button>
          <button className="ml-4 bg-gray-200 text-gray-800 px-6 py-3 rounded-lg shadow-md hover:bg-gray-300">
            Learn More
          </button>
        </div>
        <div className="mt-12">
          <h2 className="text-3xl font-bold mb-4 text-center">Room Template Generator Demo</h2>
          <div className="bg-white shadow-md rounded-lg p-6">
            <p className="text-gray-600 mb-4">
              Use the Edgar Room Template Generator to create and manage room templates for your dungeons.
            </p>
            <button className="bg-green-500 text-white px-6 py-3 rounded-lg shadow-md hover:bg-green-600">
              Generate Room Template
            </button>
          </div>
        </div>
      </div>
    </main>
  );
};

export default Index;