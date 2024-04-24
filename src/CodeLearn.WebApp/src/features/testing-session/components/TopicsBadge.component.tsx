import { Badge } from '@/components/ui/badge';
import React, { useState } from 'react';
import { ChevronLeftIcon, ChevronRightIcon } from '@radix-ui/react-icons';

interface Topic {
  name: string;
}

interface TopicsBadgeProps {
  topics: Topic[];
}

const TopicsBadge: React.FC<TopicsBadgeProps> = ({ topics }) => {
  const [showTopics, setShowTopics] = useState(false);

  const toggleTopics = () => {
    setShowTopics(!showTopics);
  };

  return (
    <>
      <button onClick={toggleTopics} className="topics-badge">
        {showTopics ? (
          <Badge variant="secondary">
            Topics <ChevronRightIcon width={14} height={14} className="mt-0.5" />
          </Badge>
        ) : (
          <Badge variant="secondary">
            Topics <ChevronLeftIcon width={14} height={14} className="mt-0.5" />
          </Badge>
        )}
      </button>
      {showTopics &&
        topics.map((topic, index) => (
          <Badge key={index} variant="secondary" className="truncate">
            {topic.name}
          </Badge>
        ))}
    </>
  );
};

export default TopicsBadge;
