// Update this page (the content is just a fallback if you fail to update the page). Always include w-full and min-h-screen classes in the main element.
import WorkflowDemo from "@/components/WorkflowDemo";

import WorkflowDemo from "@/components/WorkflowDemo";
import RoomTemplateGenerator from "@/components/RoomTemplateGenerator";
import DungeonVisualizer from "@/components/DungeonVisualizer";

const Index = () => {
  return (
    <main className="w-full min-h-screen flex flex-col items-center justify-center bg-gray-100">
      <div className="container mx-auto px-4 py-8">
        <WorkflowDemo />
        <div className="mt-12">
          <h2 className="text-3xl font-bold mb-4 text-center">Room Template Generator</h2>
          <div className="bg-white shadow-md rounded-lg p-6">
            <RoomTemplateGenerator />
          </div>
        </div>
        <div className="mt-12">
          <h2 className="text-3xl font-bold mb-4 text-center">Dungeon Visualizer</h2>
          <div className="bg-white shadow-md rounded-lg p-6">
            <DungeonVisualizer templates={[]} />
          </div>
        </div>
      </div>
    </main>
  );
};

export default Index;

export default Index;